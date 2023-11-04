clear, clc, close all;

% Velocity parameters
alpha1 = 6.8; beta1 = 3.9;
alpha2 = 8.0; beta2 = 4.6;

% Angle parameters
angle_min = 0; angle_step = 15; angle_max = 80;



Plot_Ray_Paths(alpha1, beta1, alpha2, beta2, angle_step, angle_max);