clear, clc, close all;
% 定义波速度
alpha1 = 6.8; % P波速度，单位：km/s
beta1 = 3.9;  % SV波速度，单位：km/s
alpha2 = 8.0; % P波速度，单位：km/s
beta2 = 4.6;  % SV波速度，单位：km/s

% 初始化入射角范围
theta1_deg = 0:0.1:90; % 入射角范围从0度到90度，以0.1度为步进

% 初始化反射角和透射角的数组
theta2_P_reflect_P = zeros(size(theta1_deg));
theta2_P_reflect_SV = zeros(size(theta1_deg));
theta2_P_transmit_P = zeros(size(theta1_deg));
theta2_P_transmit_SV = zeros(size(theta1_deg));

theta2_S_reflect_P = zeros(size(theta1_deg));
theta2_S_reflect_SV = zeros(size(theta1_deg));
theta2_S_transmit_P = zeros(size(theta1_deg));
theta2_S_transmit_SV = zeros(size(theta1_deg));

% 计算反射角和透射角
for i = 1:length(theta1_deg)
    theta1_rad = deg2rad(theta1_deg(i)); % 将角度转换为弧度

    % 计算P波的反射P波
    theta2_P_reflect_P(i) = asind((alpha1 / alpha1) * sin(theta1_rad));
    % 计算P波的反射SV波
    theta2_P_reflect_SV(i) = asind((beta1 / alpha1) * sin(theta1_rad));
    % 计算P波的透射P波
    theta2_P_transmit_P(i) = asind((alpha2 / alpha1) * sin(theta1_rad));
    % 计算P波的透射SV波
    theta2_P_transmit_SV(i) = asind((beta2 / alpha1) * sin(theta1_rad));

    % 计算S波的反射P波
    theta2_S_reflect_P(i) = asind((alpha1 / beta1) * sin(theta1_rad));
    % 计算S波的反射SV波
    theta2_S_reflect_SV(i) = asind((beta1 / beta1) * sin(theta1_rad));
    % 计算S波的透射P波
    theta2_S_transmit_P(i) = asind((alpha2 / beta1) * sin(theta1_rad));
    % 计算S波的透射SV波
    theta2_S_transmit_SV(i) = asind((beta2 / beta1) * sin(theta1_rad));

end



% 绘制P波的入射情况
figure;
subplot(2, 4, 1);
plot(theta1_deg, theta2_P_reflect_P, 'LineWidth', 2);
xlim([0, 90]);
ylim([0, 89.9]);
xlabel("入射角(°)");
ylabel("出射角(°)");
title("反射P波");

subplot(2, 4, 2);
plot(theta1_deg, theta2_P_reflect_SV, 'LineWidth', 2);
xlim([0, 90]);
ylim([0, 89.9]);
xlabel("入射角(°)");
ylabel("出射角(°)");
title("反射SV波");

subplot(2, 4, 3);
plot(theta1_deg, theta2_P_transmit_P, 'r', 'LineWidth', 2);
xlim([0, 90]);
ylim([0, 89.9]);
xlabel("入射角(°)");
ylabel("出射角(°)");
title("透射P波");

subplot(2, 4, 4);
plot(theta1_deg, theta2_P_transmit_SV, 'LineWidth', 2);
xlim([0, 90]);
ylim([0, 89.9]);
xlabel("入射角(°)");
ylabel("出射角(°)");
title("透射SV波");


% 绘制S波的入射情况
subplot(2, 4, 5);
plot(theta1_deg, theta2_S_reflect_P, 'r', 'LineWidth', 2);
xlim([0, 90]);
ylim([0, 89.9]);
xlabel("入射角(°)");
ylabel("出射角(°)");
title("反射P波");

subplot(2, 4, 6);
plot(theta1_deg, theta2_S_reflect_SV, 'LineWidth', 2);
xlim([0, 90]);
ylim([0, 89.9]);
xlabel("入射角(°)");
ylabel("出射角(°)");
title("反射SV波");

subplot(2, 4, 7);
plot(theta1_deg, theta2_S_transmit_P, 'r', 'LineWidth', 2);
xlim([0, 90]);
ylim([0, 89.9]);
xlabel("入射角(°)");
ylabel("出射角(°)");
title("透射P波");

subplot(2, 4, 8);
plot(theta1_deg, theta2_S_transmit_SV, 'r', 'LineWidth', 2);
xlim([0, 90]);
ylim([0, 89.9]);
xlabel("入射角(°)");
ylabel("出射角(°)");
title("透射SV波");

% 临界角
theta2_P_transmit_P_angle = rad2deg(asin(alpha1 / alpha2));
disp("%%%%%%%%%%%%%%%%%%%%%%%%%%");
disp("P-wave indident case");
fprintf("the critical angle for transmitted P-wave is: %f° \n", theta2_P_transmit_P_angle);
disp("                          ");
disp("%%%%%%%%%%%%%%%%%%%%%%%%%%");
disp("SV-wave indident case");
theta2_S_reflect_P_angle = rad2deg(asin(beta1 / alpha1));
fprintf("the critical angle for reflected P-wave is: %f° \n", theta2_S_reflect_P_angle);
theta2_S_transmit_P_angle = rad2deg(asin(beta1 / alpha1));
fprintf("the critical angle for transmitted P-wave is: %f° \n", theta2_S_transmit_P_angle);
theta2_S_transmit_SV_angle = rad2deg(asin(beta1 / beta2));
fprintf("the critical angle for transmitted SV-wave is: %f° \n", theta2_S_transmit_SV_angle);