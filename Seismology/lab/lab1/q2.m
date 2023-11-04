clear, clc;

% (1)
% yingli vector
sigma = [2, 1, 3; 1, -1, -2; 3, -2, 5];
n1 = [1, 0, 0]';
Cauchy_Formula(sigma, n1)

% (2)
% yingli vector
n2 = [3, -1, 2]';
Cauchy_Formula(sigma, n2)


% (3)
% lambda : tezhengzhi
% X : tezheng vector
lambda = eig(sigma)
[X, D] = eig(sigma)