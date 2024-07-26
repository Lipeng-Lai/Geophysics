clear, clc, close all;

%% 观测点参数(单位: m) 
% (1) X-axis
% xmin = 0;
% xmax = 20 * 1e3;
% dx = 200;
% nx = (xmax - xmin) / dx;
% x = linspace(xmin, xmax, nx);

% (2) X-axis
xmin = 0;
xmax = 50 * 1e3;
dx = 500;
nx = (xmax - xmin) / dx;
x = linspace(xmin, xmax, nx);

% (1) Y-axis
% ymin = 0;
% ymax = 20 * 1e3;
% dy = 200;
% ny = (ymax - ymin) / dy;
% y = linspace(ymin, ymax, ny);

% (2) Y-axis
ymin = 0;
ymax = 50 * 1e3;
dy = 500;
ny = (ymax - ymin) / dy;
y = linspace(ymin, ymax, ny);

% Z-axis
z = 0;

%% 球体坐标(单位: km)
% (1) 
% xc = 10 * 1e3;
% yc = 10 * 1e3;
% zc = 4 * 1e3;
% dr = 3 * 1e3;
% dDensity = 1.0; % 密度单位:g/cm^3

% (2)
xc1 = 20 * 1e3;
yc1 = 25 * 1e3;
xc2 = 30 * 1e3;
yc2 = 25 * 1e3;

zc = 18 * 1e3; % 6, 10, 13, 18
dr = 1e3;
dDensity = 1.0; % 密度单位:g/cm^3


%% 重力异常平面等值线图

Gravity2D_Deltag = zeros(length(x), length(y));
Gravity2D_Deltag_dx = zeros(length(x), length(y));
Gravity2D_Deltag_dz = zeros(length(x), length(y));
Gravity2D_Deltag_dz2 = zeros(length(x), length(y));
g_temp2 = 0;
g_dx_temp2 = 0;
g_dz_temp2 = 0;
g_dz2_temp2 = 0;

for i = 1: length(x)
    for j = 1: length(y)
        % Gravity2D_Deltag(i, j) = Sphere_Gravity_Deltag_Dxyz(x(i), y(j), z, xc, yc, zc, dr, dDensity);
        [g_temp1, g_dx_temp1, g_dz_temp1, g_dz2_temp1] = Sphere_Gravity_Deltag_Dxyz(x(i), y(j), ...
            z, xc1, yc1, zc, dr, dDensity);

        [g_temp2, g_dx_temp2, g_dz_temp2, g_dz2_temp2] = Sphere_Gravity_Deltag_Dxyz(x(i), y(j), ...
            z, xc2, yc2, zc, dr, dDensity);
        Gravity2D_Deltag(i, j) = g_temp1 + g_temp2;
        Gravity2D_Deltag_dx(i, j) = g_dx_temp1 + g_dx_temp2;
        Gravity2D_Deltag_dz(i, j) = g_dz_temp1 + g_dz_temp2;
        Gravity2D_Deltag_dz2(i, j) = g_dz2_temp1 + g_dz2_temp2;
    end
end

%% 重力异常剖面图
Gravity1D_Deltag_Xline = zeros(1, length(x));
Gravity1D_Deltag_dx_Xline = zeros(1, length(x));
Gravity1D_Deltag_dz_Xline = zeros(1, length(x));
Gravity1D_Deltag_dz2_Xline = zeros(1, length(x));

for i = 1: length(x)
    % Gravity1D_Deltag_Xline(i) = Sphere_Gravity_Deltag_Dxyz(x(i), yc, z, xc, yc, zc, dr, dDensity);
    [g_temp1, g_dx_temp1, g_dz_temp1, g_dz2_temp1] = Sphere_Gravity_Deltag_Dxyz(x(i), yc1, z, xc1, yc1, zc, dr, dDensity);
    [g_temp2, g_dx_temp2, g_dz_temp2, g_dz2_temp2] = Sphere_Gravity_Deltag_Dxyz(x(i), yc2, z, xc2, yc2, zc, dr, dDensity);
    Gravity1D_Deltag_Xline(i) = g_temp1 + g_temp2;
    Gravity1D_Deltag_dx_Xline(i) = g_dx_temp1 + g_dx_temp2;
    Gravity1D_Deltag_dz_Xline(i) = g_dz_temp1 + g_dz_temp2;
    Gravity1D_Deltag_dz2_Xline(i) = g_dz2_temp1 + g_dz2_temp2;
end

plot_line = 25 * 1e3;

%% (1) Delta_g
figure;
sgtitle("Depth = 18km 重力异常单位:mGal")

subplot(4, 2, 1);
contourf(x, y, Gravity2D_Deltag, 50, 'LineStyle', 'none');
axis equal;
set(gca, 'XLim', [xmin, xmax], 'YLim', [ymin, ymax]);
colormap('jet');
colorbar;
xlabel('x/m');
ylabel('y/m');
title("平面等值线图");

% 在 y=1000 处画一条黑色的线
hold on;  % 保持当前图形
line([xmin, xmax], [plot_line, plot_line], 'Color', 'red', 'LineWidth', 2);  % 画黑色的线
hold off;  % 释放当前图形

subplot(4, 2, 2);
plot(x, Gravity1D_Deltag_Xline, 'LineWidth', 2, 'Color', 'red');
xlabel('x/m');
ylabel('y/m');
title("重力异常剖面图");

%% (2) Delta_g_dx
subplot(4, 2, 3);
contourf(x, y, Gravity2D_Deltag_dx, 50, 'LineStyle', 'none');
axis equal;
set(gca, 'XLim', [xmin, xmax], 'YLim', [ymin, ymax]);
colormap('jet');
colorbar;
xlabel('x/m');
ylabel('y/m');
title("平面等值线图");

% 在 y=1000 处画一条黑色的线
hold on;  % 保持当前图形
line([xmin, xmax], [plot_line, plot_line], 'Color', 'red', 'LineWidth', 2);  % 画黑色的线
hold off;  % 释放当前图形

subplot(4, 2, 4);
plot(x, Gravity1D_Deltag_dx_Xline, 'LineWidth', 2, 'Color', 'red');
xlabel('x/m');
ylabel('y/m');
title("重力异常水平一阶导数剖面图");

%% (3) Delta_g_dz
subplot(4, 2, 5);
contourf(x, y, Gravity2D_Deltag_dz, 50, 'LineStyle', 'none');
axis equal;
set(gca, 'XLim', [xmin, xmax], 'YLim', [ymin, ymax]);
colormap('jet');
colorbar;
xlabel('x/m');
ylabel('y/m');
title("平面等值线图");

% 在 y=1000 处画一条黑色的线
hold on;  % 保持当前图形
line([xmin, xmax], [plot_line, plot_line], 'Color', 'red', 'LineWidth', 2);  % 画黑色的线
hold off;  % 释放当前图形

subplot(4, 2, 6);
plot(x, Gravity1D_Deltag_dz_Xline, 'LineWidth', 2, 'Color', 'red');
xlabel('x/m');
ylabel('y/m');
title("重力异常垂向一阶导数剖面图");

%% (4) Delta_g_dz2
subplot(4, 2, 7);
contourf(x, y, Gravity2D_Deltag_dz2, 50, 'LineStyle', 'none');
axis equal;
set(gca, 'XLim', [xmin, xmax], 'YLim', [ymin, ymax]);
colormap('jet');
colorbar;
xlabel('x/m');
ylabel('y/m');
title("平面等值线图 ");

% 在 y=1000 处画一条黑色的线
hold on;  % 保持当前图形
line([xmin, xmax], [plot_line, plot_line], 'Color', 'red', 'LineWidth', 2);  % 画黑色的线
hold off;  % 释放当前图形

subplot(4, 2, 8);
plot(x, Gravity1D_Deltag_dz2_Xline, 'LineWidth', 2, 'Color', 'red');
xlabel('x/m');
ylabel('y/m');
title("重力异常垂向二阶导数剖面图 ");

%% (5)
% figure;
% % plot(x, Gravity1D_Deltag_Xline);
% % hold on;
% plot(x, Gravity1D_Deltag_dx_Xline, 'LineWidth', 2);
% hold on;
% plot(x, Gravity1D_Deltag_dz_Xline, 'LineWidth', 2);
% % hold on;
% % plot(x, Gravity1D_Deltag_dz2_Xline);
% legend('\Delta g dx', '\Delta g dz');
% xlabel('x/m');
% ylabel('mGal');
% title("沿着测线上的重力异常水平/垂向一阶导数");