function [g, g_dx, g_dz, g_dz2] = Sphere_Gravity_Deltag_Dxyz(dX, dY, dZ, ...    %观测点坐标(m)
                                dXc, dYc, dZc, ...  %球体坐标(m)
                                dRadius, ...        %球体半径(m)
                                dDensity)           %球体密度(g/cm^3)
% 单位换算 (m/s^2) -> mGal
scale = 1e5;
% 单位换算 (g/cm^3) -> (kg/m^3)
dDensity = 1e3 * dDensity;
% 万有引力常数
G = 6.674e-11;

% 球体质量
m = (4/3) * pi * dRadius^3 * dDensity;

% 重力异常
g = G * m * (dZc - dZ) / ( (dXc - dX)^2 + (dYc - dY)^2 + (dZc - dZ)^2 )^(3/2);
g_dx = 3 * G * m * (dZc - dZ) * (dXc - dX) / ( (dXc - dX)^2 + (dYc - dY)^2 + (dZc - dZ)^2 )^(5/2);
g_dz = G * m * (2 * (dZc - dZ)^2 - (dXc - dX)) / ( (dXc - dX)^2 + (dYc - dY)^2 + (dZc - dZ)^2 )^(5/2);
g_dz2 = 3 * G * m * (dZc - dZ) * (2 * (dZc - dZ)^2 - (dXc - dX)^2) / ...
            ( (dXc - dX)^2 + (dYc - dY)^2 + (dZc - dZ)^2 )^(7/2);

% 单位换算成mGal
g = g * scale;
g_dx = g_dx * scale;
g_dz = g_dz * scale;
g_dz2 = g_dz2 * scale;