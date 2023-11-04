function [y] = cal_cos(t, x, w, k)

y = cos(w * t - k * x);


if length(t) > length(x)
    plot(t, y);
    xlabel('t');
    ylabel('y = cos');
else
    plot(x, y);
    xlabel('x');
    ylabel('y = cos');
end

end