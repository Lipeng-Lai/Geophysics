clear, clc, close all;
%% (1) 水平层状介质速度剖面图
% parameters
velocity = [2.0, 5.0; 5.0, 8.0;
    8.0, 6.0; 6.0, 9.0;
    9.0, 12.0; 12.0, 30.0];
height = [40, 40, 40, 40, 40, 240];
dz = 0.1;
angles = [7:0.1:15, 15:1:40];

PresumHeight = zeros(1, length(height)); % 初始化 PresumHeight

PlotVelocity = velocity(:, 1);
PresumHeight(1) = height(1);
for i = 2: length(height)
    PresumHeight(i) = PresumHeight(i - 1) + height(i); 
end

% disp(PresumHeight);
figure;
plot(PlotVelocity, PresumHeight, "LineWidth", 2, "Color", "blue");
set(gca, 'Ydir', 'reverse'); % 反转y轴
ylim([0, 450]);
xlim([2, 12]);
xlabel("速度(km/s)");
ylabel("深度(km)");
title("水平层状介质速度剖面图");


%% (2) 射线路径追踪图
% initialization
n = length(height);
d_ray = PresumHeight(n) / dz;
XPoints = zeros(length(angles)); % 距离
TPoints = zeros(length(angles)); % 时间
PPoints = zeros(length(angles)); % 射线参数
TauPoints = zeros(length(angles)); % 截距
% Loop over the main layers
figure;
subplot(2, 2, 1);
nLayers = length(height);

for angleIdx = 1: length(angles)
    angle = angles(angleIdx);
    p = sind(angle) / velocity(1, 1);
    
    % initialization
    dep_all_i = 0;
    Tp = 0;
    Xp = 0;
    raypath = zeros(d_ray, 2);
    idx = 1;
    
    % 正向传播
    for i = 1: nLayers
        v1 = velocity(i, 1);
        v2 = velocity(i, 2);
        nz = floor(height(i) / dz); % 厚度微元格子个数
        dv = (v2 - v1) / nz; % 速度微元大小
        dmax = Depth_Max(velocity, height, angle);

        % Loop over the sublayers
        for j = 1: nz
            depth = dep_all_i + j * dz; % 此时处于的深度
            v_i = v1 + (j - 1) * dv; % 射线到达该厚度微元时的速度
            
            if (depth <= dmax) 
                eta = sqrt(1 / v_i / v_i - p*p);
                x_i = dz * p / eta; % calculate the travelled horizontal distance
                raypath(idx + 1, 1) = raypath(idx, 1) + x_i; % horiontal distance
                raypath(idx + 1, 2) = raypath(idx, 2) + dz; % vertical depth
                Xp = Xp + x_i;
                Tp = Tp + sqrt(dz*dz + x_i*x_i) / v_i;

                idx = idx + 1;
            else
                break;
            end
        end
        dep_all_i = dep_all_i + height(i);
    end
    plot(raypath(:, 1), raypath(:, 2));
    hold on;
    
    % 反向传播
    for i = nLayers: -1: 1
        v1 = velocity(i, 1);
        v2 = velocity(i, 2);
        nz = floor(height(i) / dz); % 厚度微元格子个数
        dv = (v2 - v1) / nz; % 速度微元大小
        dmax = Depth_Max(velocity, height, angle);

        % Loop over the sublayers
        for j = nz: -1: 1
            depth = dep_all_i - (nz - j + 1) * dz; % 此时处于的深度
            v_i = v2 - (nz - j + 1) * dv; % 射线到达该厚度微元时的速度
            if (depth <= dmax) 
                eta = sqrt(1 / v_i / v_i - p*p);
                x_i = dz * p / eta; % calculate the travelled horizontal distance
                raypath(idx + 1, 1) = raypath(idx, 1) + x_i; % horiontal distance
                raypath(idx + 1, 2) = raypath(idx, 2) - dz; % vertical depth
                                
                Xp = Xp + x_i;
                Tp = Tp + sqrt(dz*dz + x_i*x_i) / v_i;
                idx = idx + 1;
            end
        end
        dep_all_i = dep_all_i - height(i);
    end
    plot(raypath(:, 1), raypath(:, 2));
    hold on;
    
    % Array store the value
    XPoints(angleIdx) = Xp;
    TPoints(angleIdx) = Tp;
    PPoints(angleIdx) = p;
    TauPoints(angleIdx) = Tp - p * Xp;
end

ylim([0, 300]);
set(gca, 'Ydir', 'reverse'); % 反转y轴
hold off;
xlabel('水平距离（km）');
ylabel('深度（km）');
title('射线路径追踪图')


%% (3) 走时曲线(T-x)图
subplot(2, 2, 2);
scatter(XPoints, TPoints, 'filled', 'r', 'SizeData', 12); % 36是填充圆的大小
ylim([20, 110]);
xlabel('水平距离（km）');
ylabel('时间');
title('走时曲线图[T-x]');

%% (4) 射线参数随偏移距变化(p-x)图
subplot(2, 2, 3);
scatter(XPoints, PPoints, 'filled', 'm', 'SizeData', 12); % 36是填充圆的大小
ylim([0.05, 0.35]);
xlabel('水平距离（km）');
ylabel('射线参数(s/km)');
title('射线参数随偏移距变化图[p-x]');

%% (5) 截距-射线参数(\tau - p)图
subplot(2, 2, 4);
scatter(PPoints, TauPoints, 'filled', 'SizeData', 12); % 36是填充圆的大小
ylim([0.05, 70]);
xlabel('射线参数(s/km)');
ylabel('截距');
title('截距-射线参数图[\tau-p]');
