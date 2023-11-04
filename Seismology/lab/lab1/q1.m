clear, clc, close all;
x = [4, 5, 6, 7, 8, 9];
y = [304616, 40376, 3439, 335, 27, 2];
y = log(y);

% scatter
scatter(x, y);
xlim([3, 10])
hold on;

% cal b value
X = [ones(length(y), 1), x'];
Y = y';
[b, bint, r, rint, stats]= regress(Y, X);
b, bint, stats

% plot 
z = b(1) + b(2) * x;
plot(x, z)
xlabel("震级")
ylabel("数量")
title("回归分析震级与数量关系")
legend('数据点', '回归直线');