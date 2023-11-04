function [sigma] = strain2strees(e, lambda, mu)


[n, m] = size(e);
sigma = zeros(n, m);

theta = 0;
for i = 1: m
    theta = theta + e(i, i);

for i = 1: n
    for j = 1: m
        sigma(i, j) = lambda * theta + 2 * mu * e(i, j);
    end
end

end