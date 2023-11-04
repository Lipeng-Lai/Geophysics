function Plot_Ray_Paths(alpha1, beta1, alpha2, beta2, angle_step, angle_max)

figure;
hold on;

% plot line
line([-1, 17], [0, 0], 'LineStyle', '--', 'Color', 'k');

% Source
source_x = 0; source_y = 2;
end_x = source_x;
end_y = source_y;

for angle = 0: angle_step: angle_max
    % degree to rad
    angle_rad = deg2rad(angle);
    
    slope = tan(angle_rad);
    if slope == 0
        end_x = source_x;
    else
        end_x = source_x + source_y * slope;
    end
    
    % 
    end_y = 0;

    % plot incident rays
    quiver(source_x, source_y, end_x - source_x, end_y - source_y, 1, 'black', 'LineWidth', 1.5);

    % plot reflect P wave rays
%     reflect_P_x = end_x + end_x;
%     reflect_P_y = source_y;
    reflect_P_y = 1.75;
    reflect_P_x = reflect_P_y * slope;
    quiver(end_x, end_y, reflect_P_x, reflect_P_y - end_y, 1, 'blue', 'LineWidth', 1.5);

    % plot reflect SV wave rays
    refle_SV_angle = asin(beta1 * sin(angle_rad) / alpha1);
%     refle_SV_x = end_x * sin(refle_SV_angle);
%     refle_SV_y = refle_SV_x / tan(refle_SV_angle);
%     if refle_SV_angle == 0
%         refle_SV_y = source_y;
%     end
    refle_SV_y = 1.75;
    refle_SV_x = refle_SV_y * tan(refle_SV_angle);
    quiver(end_x, end_y, refle_SV_x, refle_SV_y - end_y, 1, 'green', 'LineWidth', 1.5);

    % Plot transmitted P wave rays
    trans_P_angle = asin(alpha2 * sin(angle_rad) / alpha1);
    
%     trans_P_x = end_x * sin(trans_P_angle);
%     trans_P_y = trans_P_x / tan(trans_P_angle);
    trans_P_y = 1.75;
    trans_P_x = trans_P_y * tan(trans_P_angle);
    if rad2deg(trans_P_angle) < 90
        quiver(end_x, end_y, trans_P_x, -(trans_P_y - end_y), 1, 'red', 'LineWidth', 1.5);
    end

    % Plot transmitted SV wave rays
    trans_SV_angle = asin(beta2 * sin(angle_rad) / alpha1);
%     trans_SV_x = end_x * sin(trans_SV_angle);
%     trans_SV_y = trans_SV_x / tan(trans_SV_angle);
    trans_SV_y = 1.75;
    trans_SV_x = trans_SV_y * tan(trans_SV_angle);
    quiver(end_x, end_y, trans_SV_x, -(trans_SV_y - end_y), 1, 'cyan', 'LineWidth', 1.5);
end


axis([-1, 15, -2.5, 2.5]);
annotation('textarrow', [0.81 0.87], [0.47 0.47], 'String', 'incident P', ...
    'LineWidth', 2, 'color', 'k', 'FontSize', 14);
annotation('textarrow', [0.81 0.87], [0.40 0.40], 'String', 'reflected P', ...
    'LineWidth', 2, 'color', 'blue', 'FontSize', 14);
annotation('textarrow', [0.81 0.87], [0.32 0.32], 'String', 'reflected SV', ...
    'LineWidth', 2, 'color', 'green', 'FontSize', 14);
annotation('textarrow', [0.81 0.87], [0.25 0.25], 'String', 'transmitted P', ...
    'LineWidth', 2, 'color', 'red', 'FontSize', 14);
annotation('textarrow', [0.81 0.87], [0.18 0.18], 'String', 'transmitted SV', ...
    'LineWidth', 2, 'color', 'cyan', 'FontSize', 14);
xlabel('X轴');
ylabel('Y轴');
title("P wave impinges on the solid-solid interface");