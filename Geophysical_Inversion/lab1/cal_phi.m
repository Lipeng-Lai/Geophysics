function [phi] = cal_phi(z0, zk, l, alpha, xk, x0, b, flag)

eps = 1e-15;
up = 0;
down = 0;
if flag == 1
    up = z0 - zk - l*sin(alpha);
    down = xk - x0 + b + l*cos(alpha);
elseif flag == 2
    up = z0 - zk + l*sin(alpha);
    down = xk - x0 + b - l*cos(alpha);
elseif flag == 3
    up = z0 - zk - l*sin(alpha);
    down = xk - x0 - b + l*cos(alpha);
elseif flag == 4
    up = z0 - zk + l*sin(alpha);
    down = xk - x0 - b - l*cos(alpha);
end

res = atan(up/down);
if abs(down) > eps
    if up > eps && down > eps
        phi = res;
    elseif up < -eps && down < -eps
        phi = -pi + res;
    elseif (up/down) < eps
        phi = pi + res;
    
    end
elseif abs(down) < eps
    if up < eps
        phi = -pi/2;
    elseif up > eps
        phi = pi/2;
    end
end

phi = pi - phi;

end