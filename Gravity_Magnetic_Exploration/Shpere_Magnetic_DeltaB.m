function [DB1, DB2] = Shpere_Magnetic_DeltaB(x, y, z, ...   % 观测点坐标(单位:m)
                                             xc, yc, zc, ...% 球心坐标(单位:m)
                                             r, k, A, ...   % 球心半径r, 磁化率k(SI)单位, X轴正方向方位角A
                                             Mr, Dr, Ir, ...% 剩余磁化强度大小Mr(A/m), 偏角(°), 倾角(°)
                                             B0, D, I)      % 背景场大小B0(nT), 地磁偏角D(°), 地磁倾角I(°)

MU = 4*pi*1e-7; % 真空磁导率
NT2T = 1e-9; % 磁异常单位转换系数(nT->T)
T2NT = 1e9; % (T->nT)

% 单位转换
A = deg2rad(A);
Dr = deg2rad(Dr);
Ir = deg2rad(Ir);
B0 = B0 * NT2T;
D = deg2rad(D);
I = deg2rad(I);

% 背景场的方向余弦
Nx = cos(I) * cos(D - A);
Ny = cos(I) * sin(D - A);
Nz = sin(I);

% 感应磁化强度大小
Mi = k * B0 / MU;

% 剩余磁化强度的方向余弦
Nrx = cos(Ir) * cos(Dr - A);
Nry = cos(Ir) * sin(Dr - A);
Nrz = sin(Ir);

% 总磁化强度各分量
Mx = Mi * Nx + Mr * Nrx;
My = Mi * Ny + Mr * Nry;
Mz = Mi * Nz + Mr * Nrz;

% 几何引力位二次偏导数
DX = xc - x;
DY = yc - y;
DZ = zc - z;
Vxx = Vaa(DX, DY, DZ);
Vyy = Vaa(DY, DZ, DX);
Vzz = Vaa(DZ, DX, DY);
Vxy = Vab(DX, DY, DZ);
Vyz = Vab(DY, DZ, DX);
Vzx = Vab(DZ, DX, DY);

% 计算磁异常各分量
C = MU * r^3 / 3;
Bax = Mx * Vxx + My * Vxy + Mz * Vzx;
Bay = Mx * Vxy + My * Vyy + Mz * Vyz;
Baz = Mx * Vzx + My * Vyz + Mz * Vzz;

% 计算背景场各分量
B0x = B0 * Nx;
B0y = B0 * Ny;
B0z = B0 * Nz;

DB1 = T2NT * (sqrt( (B0x + Bax)^2 + (B0y + Bay)^2 + (B0z+Baz)^2 ) - B0);
DB2 = T2NT * (Bax * Nx + Bay * Ny + Baz * Nz);
end

function res = Vaa(A, B, C)
res = (2 * A^2 - B^2 - C^2) / (A^2 + B^2 + C^2)^(5/2);
end

function res = Vab(A, B, C)
res = (3 * A * B) / (A^2 + B^2 + C^2)^(5/2);
end