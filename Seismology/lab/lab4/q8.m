clear, clc, close all;
height = [40, 50, 60, 80]; % 地层厚度 单位km
velocity = [6.3, 6.8, 7.5, 8.2]; % 层速度 单位km/s

% 双曲线近似方程
tn1 = 0; tn2 = 0; V_Delta_t = 0;

XPoints = zeros(4, 26);
TPoints = zeros(4, 26);

for i = 1 :length(velocity) % 对地层进行循环
    % 实际层状介质时距曲线
    cnt = 1;
    for angle = 0: 2: 50
        rad = deg2rad(angle);
        rad = asin(velocity(i) * sin(rad) / velocity(1)); % Snell's law
        
        xp = height(i) * tan(rad); % 水平距离
        Tp = height(i) / (velocity(i) * cos(rad)); % 该层的实际旅行时
        XPoints(i, cnt) = 2 * xp;
        TPoints(i, cnt) = 2*Tp;

        Delta_t1 = height(i) / velocity(i); % 双曲线层时间
        Delta_t2 = 2 * height(i) / velocity(i); % 对称:双曲线层时间
        v_delta_t = velocity(i)^2 * Delta_t1; % 均方根速度分子

        cnt = cnt + 1;
    end
    tn1 = tn1 + Delta_t1; % 双曲线层时间累加
    tn2 = tn2 + Delta_t2; % 对称: 双曲线层时间累加
    V_Delta_t = V_Delta_t + v_delta_t; % 均方根速度分子


    switch i
        case 1
            scatter(XPoints(1, :), TPoints(1,:), 20, "red", 'filled');
        case 2
            scatter(sum(XPoints(1:2, :)), sum(TPoints(1:2, :)), 20, "green", 'filled');
        case 3
            scatter(sum(XPoints(1:3, :)), sum(TPoints(1:3, :)), 20, "blue", "filled");
        case 4
            scatter(sum(XPoints(1:4, :)), sum(TPoints(1:4, :)), 20, "cyan", 'filled');
    end

    V_n = V_Delta_t / tn1; % 均方根速度
    % fprintf("均方根速度vn: %f\n", V_n);
    switch i
        case 1
            offset = 0: 1: XPoints(1, 26);
        case 2
            offset = 0: 1: sum(XPoints(1:2, 26));
        case 3
            offset = 0: 1: sum(XPoints(1:3, 26));
        case 4
            offset = 0: 1: sum(XPoints(1:4, 25));
    end
    % 双曲线近似方程
    TX = sqrt(offset.^2 / V_n + tn2^2);
    hold on;
    plot(offset, TX, 'Color', 'black', 'LineWidth', 1);

end

xlim([0, 800]);
ylim([0, 140]);
xlabel("Distance(km)");
ylabel("Travel time(s)");
title("True traveltime(dots) vs. approximated traveltime(curves)");