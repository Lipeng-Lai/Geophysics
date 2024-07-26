syms x y z xs ys zs R G sigma;

% 计算距离 r
r = sqrt((xs - x)^2 + (ys - y)^2 + (zs - z)^2);

% 当 r >= R 时的偏导数计算
dVdx_large_r = -4 * pi * G * sigma * R^3 * (xs - x) / (3 * (r^3));
% 当 r < R 时的偏导数计算
dVdx_small_r = -4 * pi * G * sigma * (xs - x) / (3 * r);

% 计算二阶偏导数 d2Vdx2
d2Vdx2_large_r = simplify(diff(dVdx_large_r, x));
d2Vdx2_small_r = simplify(diff(dVdx_small_r, x));

% 计算混合偏导数 d2Vdydx
d2Vdydx_large_r = simplify(diff(dVdx_large_r, y));
d2Vdydx_small_r = simplify(diff(dVdx_small_r, y));

% 输出结果
disp('当 r >= R 时:');
disp('二阶偏导数 d2Vdx2:');
disp(d2Vdx2_large_r);
disp('混合偏导数 d2Vdydx:');
disp(d2Vdydx_large_r);

disp('当 r < R 时:');
disp('二阶偏导数 d2Vdx2:');
disp(d2Vdx2_small_r);
disp('混合偏导数 d2Vdydx:');
disp(d2Vdydx_small_r);
