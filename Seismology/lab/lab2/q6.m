clear, clc, close all;

% up
alpha1 = 13.7;
beta1 = 7.2;
rho1 = 5.5;

% lower
alpha2 = 8.0;
beta2 = 0;
rho2 = 9.9;

% wave type : p is 0, s is 1
% travel type : Down is 0, up is 1

% Downgoing P-wave
Refle_Trans_Coeff(alpha1, ...
    beta1, rho1, alpha2, beta2, rho2, 0, 0);

disp("                                    ");

% Upgoing P-wave
Refle_Trans_Coeff(alpha1, ...
    beta1, rho1, alpha2, beta2, rho2, 0, 1);

disp("                                    ");

% Downgoing S-wave
Refle_Trans_Coeff(alpha1, ...
    beta1, rho1, alpha2, beta2, rho2, 1, 0);

disp("                                    ");

% Upgoing S-wave
Refle_Trans_Coeff(alpha1, ...
    beta1, rho1, alpha2, beta2, rho2, 1, 1);