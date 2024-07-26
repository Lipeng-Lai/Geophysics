function [Za] = Sphere_DB(dX, dY, dZ, ...    %观测点坐标(m)
                       dXc, dYc, dZc, ... %球体坐标(m)
                       m, ...             %磁矩
                       I, D)              %磁倾角, 磁偏角

u0 = 4 * pi * (10e-7);      % 真空磁导率
X = (dX - dXc);
Y = (dY - dYc);
R = (dZ - dZc);
A = D;

% Hax = u0.*m.*((2*X.^2-Y.^2-R.^2).*cosd(I).*cosd(A)-3.*R.*X.*cosd(I).*sind(A)+3.*X.*Y.*cosd(I).*sind(A))./(4.*pi.*((X.^2+Y.^2+R.^2).^(2/5)));
% Hay = u0.*m.*((2*Y.^2-X.^2-R.^2).*cosd(I).*cosd(A)-3.*R.*Y.*cosd(I).*sind(A)+3.*X.*Y.*cosd(I).*cosd(A))./(4.*pi.*((X.^2+Y.^2+R.^2).^(2/5)));
Za  = u0.*m.*((2*R.^2-X.^2-Y.^2).*sind(I)-3.*R.*X.*cosd(I).*sind(A)+3.*R.*Y.*cosd(I).*sind(A))./(4.*pi.*((X.^2+Y.^2+R.^2).^(2/5))); 