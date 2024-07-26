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

%% 场源参数
xc = 25 * 1e3;
yc = 25 * 1e3;
zc = 15 * 1e3;
r = 6 * 1e3;

%% 磁场参数
k = 0.001; % 磁化率(SI单位)
A = 0; % X轴正方向的方位角
Mr = 0.0;
Dr = 0.0;
Ir = 0.0;
B0 = 65 * 1e3; % 背景场大小 (单位: nT)
D = 0; % 背景场地磁偏角


%% 计算标量总场磁异常
DB1 = zeros(length(x), length(y));
DB2 = zeros(length(x), length(y));

% 剖面
DB_p1 = zeros(1, length(x));
DB_p2 = zeros(1, length(x));
DB_sum_p1 = zeros(7, length(x));
DB_sum_p2 = zeros(7, length(x));

for I = 0: 15: 90
    DB1 = zeros(length(x), length(y));
    DB2 = zeros(length(x), length(y));
    DB_p1 = zeros(1, length(x));
    DB_p2 = zeros(1, length(x));

    for i = 1: length(x)
        [DB_p1(i), DB_p2(i)] = Shpere_Magnetic_DeltaB(x(i), yc, z, ... % 观测点坐标 x(i), y(j), z
                                                            xc, yc, zc, ...    % 场源坐标 xc, yc, zc
                                                            r, k, A, ...       % 球体半径r, 磁化率k, X轴方向方位角
                                                            Mr, Dr, Ir, ...    % 剩余磁化强度大小Mr(A/m), 偏角(°), 倾角(°)
                                                            B0, D, I);         % 背景场大小T, 地磁偏角D, 地磁倾角I
        DB_sum_p1((I/15)+1, i) = DB_p1(i);
        DB_sum_p2((I/15)+1, i) = DB_p2(i);
        for j = 1: length(y)
            [DB1(i, j), DB2(i, j)] = Shpere_Magnetic_DeltaB(x(i), y(j), z, ... % 观测点坐标 x(i), y(j), z
                                                            xc, yc, zc, ...    % 场源坐标 xc, yc, zc
                                                            r, k, A, ...       % 球体半径r, 磁化率k, X轴方向方位角
                                                            Mr, Dr, Ir, ...    % 剩余磁化强度大小Mr(A/m), 偏角(°), 倾角(°)
                                                            B0, D, I);         % 背景场大小T, 地磁偏角D, 地磁倾角I
        end
    end

    figure;
    sgtitle(sprintf("倾角为%d°时的磁异常, 单位:nT", I));
    subplot(2, 2, 1);
    plot(x, DB_p1, '*r', 'LineWidth', 2.5);
    hold on;
    plot(x, DB_p2, 'b', 'LineWidth', 1.5);
    hold off;
    xlabel('X/m');
    ylabel('nT');
    legend('基于定义式', '基于物理意义');


    subplot(2, 2, 2);
    contourf(x, y, DB2, 20);
    title("磁异常沿背景场方向投影");
    xlabel('X/m');
    ylabel('Y/m');
    colormap('jet');
    colorbar;

    subplot(2, 2, 3);
    contourf(x, y, DB1, 20);
    title("总磁异常");
    xlabel('X/m');
    ylabel('Y/m');
    colormap('jet');
    colorbar;

    subplot(2, 2, 4);
    contourf(x, y, (DB1-DB2), 20);
    title("总磁异常与其背景场方向上投影之间的差异");
    xlabel('X/m');
    ylabel('Y/m');
    colormap('jet');
    colorbar;
end

figure;
for I = 0: 15: 90
    plot(x, DB_sum_p1((I/15)+1, :), 'LineWidth', 2);
    hold on;
end
title("沿剖面Y=25km Z=0km基于定义式的磁异常");
xlabel('X/m');
ylabel('磁异常的大小 nT');
legend('0°', '15°', '30°', '45°', '60°', '75°', '90°');
hold off;


figure;
for I = 0: 15: 90
    plot(x, DB_sum_p2((I/15)+1, :), 'LineWidth', 2);
    hold on;
end
title("沿剖面Y=25km Z=0km基于物理意义的磁异常");
xlabel('X/m');
ylabel('磁异常的大小 nT');
legend('0°', '15°', '30°', '45°', '60°', '75°', '90°');
hold off;

