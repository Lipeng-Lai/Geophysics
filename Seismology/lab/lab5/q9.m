clear, clc, close all;
%% (1) 画出可控源扫描信号
f1 = 1; % 单位Hz
f2 = 15; % 单位Hz
T = 5; % 单位s
dt = 0.0025; % 单位s
t = 0: dt: 2;

w = cos(2*pi*(f1 * t + ((f2 - f1)/(2*T)) *t.^2));
figure;
plot(t, w, 'Color', 'black', 'LineWidth', 1);
hold on;
quiver(0, 0, 2.2, 0, 1, 'color', 'black', 'LineWidth', 2);
xlim([0, 2.2]);
% 添加文本标签 "Time"
text(3.1, -0.1, 'Time', 'HorizontalAlignment', 'center', 'VerticalAlignment', 'middle');
title("Sweep signal");

%% (2) 可控源扫描信号的自相关
L = length(w);
autocorr_w = xcorr(w, 'coeff'); % 计算信号的自相关

% 绘制自相关图像
t_autocorr = -dt*(L-1): dt: dt*(L-1);
figure;
plot(t_autocorr, autocorr_w, 'Color', 'blue', 'LineWidth', 1);
hold on;
quiver(-4, 0, 8, 0, 1, 'MaxHeadSize', 0.1, 'color', 'black', 'LineWidth', 2);
text(3.5, -0.1, 'Lag time', 'HorizontalAlignment', 'center', 'VerticalAlignment', 'middle');
xlim([-4, 4]);
title("Klauder wavelet");

%% (3) 根据速度, 密度, 地层厚度计算得到反射序列波形
velocity = [8.3, 7.8, 8.6, 7.3]; % 速度
rho = [4.1, 3.5, 4.6, 4.0]; % 密度
height = [2, 3, 2]; % 地层厚度
tlen = 2;
t0 = 0;
[t, amp] = Make_Reflector_Series(velocity, rho, height, t0, tlen, dt);
figure;
plot(t, amp, 'Color', 'blue', 'LineWidth', 2);
hold on;
line([0, 2], [0, 0], 'color', 'black', 'Linewidth', 2);
ylim([-0.2, 0.2]);
xlabel("时间(s)");
ylabel("振幅");
title("反射序列(reflector series r[t])");

%% (4) 将(1)中的地震子波与(3)中的反射序列做卷积, 得到合成地震记录
synthetic_seismogram = conv(w, amp, 'same');
figure;
plot(t, synthetic_seismogram, 'Color', 'blue', 'LineWidth', 2);
xlabel("时间(s)");
ylabel("振幅");
title("合成地震记录");

%% (5) 对(4)中的合成记录进行反卷积, 恢复出反射序列
recovered_reflection = deconvwnr(synthetic_seismogram, w);
figure;
plot(t, recovered_reflection, 'Color', 'red', 'LineWidth', 2);
xlabel("时间(s)");
ylabel("振幅");
title("恢复出的反射序列");

%% (6) 将地震子波与合成地震记录做互相关, 恢复出反射序列, 同时与反卷积结果进行对比分析
xcorr_result = xcorr(synthetic_seismogram, w, 'coeff');
recovered_reflection_xcorr = xcorr_result(numel(synthetic_seismogram):end);

figure;
subplot(2, 1, 1);
plot(t, recovered_reflection_xcorr, 'Color', 'blue', 'LineWidth', 2);
xlabel("时间(s)");
ylabel("振幅");
title("通过互相关恢复的反射序列");

subplot(2, 1, 2);
plot(t, recovered_reflection, 'Color', 'red', 'LineWidth', 2);
xlabel("时间(s)");
ylabel("振幅");
title("通过反卷积恢复的反射序列");
