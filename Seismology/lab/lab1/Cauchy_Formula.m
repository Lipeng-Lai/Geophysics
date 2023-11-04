function [traction, normal, shear] = Cauchy_Formula(sigma, n)

traction = sigma * n;

normal = n' * traction * n;

shear = traction - normal;

end