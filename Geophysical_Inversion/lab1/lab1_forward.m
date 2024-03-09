clear, clc, close all;

% 参数
x0 = 1000.0;
z0 = 1000.0;
b = 200;
l = 200;
angle_alpha = 45;
angle_i = 45;
M = 2000.0; % 磁化强度
xk = linspace(0, 2000, 101); % xk 即P点的横坐标
zk = 0; % P点的纵坐标始终为0
sigma = 2.67; % 剩余密度大小
G = 6.67e-5;

% 转化为弧度制
rad_alpha = deg2rad(angle_alpha);
rad_angle_i = deg2rad(angle_i);
z1 = z0 - l * sin(rad_angle_i);
z2 = z0 + l * sin(rad_angle_i);

% 创建不同位置P点所受磁异常的X分量和Y分量
DeltaX = zeros(size(xk));
DeltaY = zeros(size(xk));
DeltaZ = zeros(size(xk));
Deltag = zeros(size(xk));
DeltaT = zeros(size(xk));

% 奇异值特判


% 对P点位置进行枚举循环
for index = 1: length(xk)
    r1 = sqrt((xk(index) - x0 + b + l*cos(rad_alpha)).^2 + (z0 - zk - l*sin(rad_alpha)).^2);
    r2 = sqrt((xk(index) - x0 + b - l*cos(rad_alpha)).^2 + (z0 - zk + l*sin(rad_alpha)).^2);
    r3 = sqrt((xk(index) - x0 - b + l*cos(rad_alpha)).^2 + (z0 - zk - l*sin(rad_alpha)).^2);
    r4 = sqrt((xk(index) - x0 - b - l*cos(rad_alpha)).^2 + (z0 - zk + l*sin(rad_alpha)).^2);
    
    % 决定是否奇异
    % phi1 = pi - atan((z0 - zk - l*sin(rad_alpha)) / (xk(index) - x0 + b + l*cos(rad_alpha)));
    % phi2 = pi - atan((z0 - zk + l*sin(rad_alpha)) / (xk(index) - x0 + b - l*cos(rad_alpha)));
    % phi3 = pi - atan((z0 - zk - l*sin(rad_alpha)) / (xk(index) - x0 - b + l*cos(rad_alpha)));
    % phi4 = pi - atan((z0 - zk + l*sin(rad_alpha)) / (xk(index) - x0 - b - l*cos(rad_alpha)));
    phi1 = cal_phi(z0, zk, l, rad_alpha, xk(index), x0, b, 1);
    phi2 = cal_phi(z0, zk, l, rad_alpha, xk(index), x0, b, 2);
    phi3 = cal_phi(z0, zk, l, rad_alpha, xk(index), x0, b, 3);
    phi4 = cal_phi(z0, zk, l, rad_alpha, xk(index), x0, b, 4);

    res_X = (M / 2*pi) * sin(rad_alpha) * (log(r2 * r3) / (r1 * r4) * cos(rad_alpha - rad_angle_i) - ...
        sin(rad_alpha - rad_angle_i) * (phi1 - phi2 - phi3 + phi4));
    res_Y = 0;
    res_Z = (M / 2*pi) * sin(rad_alpha) * (sin(rad_alpha - rad_angle_i) * log(r2 * r3) / (r1 * r4)) + ...
        cos(rad_alpha - rad_angle_i) * (phi1 - phi2 - phi3 + phi4);

    % 重力异常
    left = z2*(phi2 - phi4) - z1*(phi1 - phi3);
    mid = xk(index) * (sin(rad_alpha).^2 * log((r2* r3) / (r1*r4)) + cos(rad_alpha) * sin(rad_alpha) * (phi1 - phi2 - phi3 + phi4));
    right = 2 * b * (sin(rad_alpha).^2 * log(r4/r3) + cos(rad_alpha) * sin(rad_alpha) * (phi3 - phi4));

    res_g = 2 * G * sigma * (left + mid + right);
    Deltag(index) = res_g;
    
    % 磁异常分量
    DeltaX(index) = res_X;
    DeltaY(index) = res_Y;
    DeltaZ(index) = res_Z;

    D = deg2rad(atan(res_Y / res_X));
    I = deg2rad(atan(res_Z / (res_X.^2 + res_Y)));

    res_T = res_X * cos(I) * cos(D) + res_Y * cos(I) * sin(D) + res_Z * sin(I);
    DeltaT(index) = res_T;
    
end

% X坐标是xk
% 重力异常 g
subplot(3, 1, 1);
plot(xk, Deltag);
xlabel("P点的横坐标");
ylabel("重力异常大小");
title("重力异常");

% X坐标是xk
% 磁异常 T
subplot(3, 1, 2);
plot(xk, DeltaX);
xlabel("P点的横坐标");
ylabel("磁异常大小");
title("磁异常的X分量");

subplot(3, 1, 3);
plot(xk, DeltaZ);
xlabel("P点的横坐标");
ylabel("磁异常大小");
title("磁异常的Z分量");