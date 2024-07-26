clear, clc, close all;

%% 观测面参数
% X-axis
xmin = 0;
xmax = 50 * 1e3;
dx = 500;
nx = (xmax - xmin) / dx;
x = linspace(xmin, xmax, nx);

% Y-axis
ymin = 0;
ymax = 50 * 1e3;
dy = 500;
ny = (ymax - ymin) / dy;
y = linspace(ymin, ymax, ny);

% Z-axis
z = 0;

%% 球体模型参数
xc = 25 * 1e3;
yc = 25 * 1e3;
zc = 15 * 1e3;
r = 6 * 1e3;

%% 磁场参数
k = 0.001; % 磁化率(SI单位)
alpha = 0; % X轴正方向的方位角
T = 65 * 1e3; % 背景场大小 (单位: nT)
D = 0; % 背景场地磁偏角


%% 推导得到的参数
M =  k * T; % 球体磁化强度
V = (3 * pi * r.^3) / 4; % 球体的体积
m = V * M; % 磁矩

%% 核心计算过程
Magnetic2D_DeltaT = zeros(length(x), length(y));
Magnetic1D_DeltaT = zeros(length(x), 1);
Magnetic1D_DeltaT_sum = zeros(length(x), 7);

for I = 0: 15: 90
    Magnetic2D_DeltaT = zeros(length(x), length(y)); % 重新初始化
    Magnetic1D_DeltaT = zeros(length(x), 1);
    for i = 1: length(x)
        for j = 1: length(y)
            Magnetic2D_DeltaT(i, j) = Sphere_DB(x(i), y(j), z, xc, yc, zc, m, I, D);
        end
        Magnetic1D_DeltaT(i) = Sphere_DB(x(i), yc, z, xc, yc, zc, m, I, D);
        Magnetic1D_DeltaT_sum(i, (I / 15) + 1) = Sphere_DB(x(i), yc, z, xc, yc, zc, m, I, D);
    end
    figure;
    sgtitle(sprintf("地磁倾角为%d°的磁异常", I));
    subplot(1, 2, 1);
    contourf(x, y, Magnetic2D_DeltaT, 50, 'LineStyle', 'none');
    axis equal;
    set(gca, 'XLim', [xmin, xmax], 'YLim', [ymin, ymax]);
    colormap('jet');
    colorbar;
    xlabel('x/m');
    ylabel('y/m');
    title("磁异常平面等值线图");
    % 画图
    
    % 在 y=1000 处画一条黑色的线
    hold on;  % 保持当前图形
    line([xmin, xmax], [25 * 1e3, 25 * 1e3], 'Color', 'red', 'LineWidth', 2);  % 画黑色的线
    hold off;  % 释放当前图形
    
    subplot(1, 2, 2);
    plot(x, Magnetic1D_DeltaT, 'LineWidth', 2, 'Color', 'red');
    xlabel('x/m');
    ylabel('y/m');
    title("磁异常剖面图");

    % surf(x, y, Magnetic2D_DeltaT);
end

figure;
for I = 0: 15: 90
    plot(x, Magnetic1D_DeltaT_sum(:, (I / 15) + 1), 'LineWidth', 2);
    hold on;
end
xlabel("X (m)")
ylabel("磁异常大小 (nT)")
title("沿测线Y=25km, Z=0km的不同磁倾角的磁异常大小");
legend('0°', '15°', '30°', '45°', '60°', '75°', '90°');
hold off;