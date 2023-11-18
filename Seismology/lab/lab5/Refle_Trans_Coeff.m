function [refle_coeff, trans_coeff] = Refle_Trans_Coeff(v1, rho1, v2, rho2)

refle_coeff = (rho1 * v1 - rho2 * v2) / (rho1 * v1 + rho2 * v2);
trans_coeff = 2*rho1*v1 / (rho1*v1 + rho2*v2);

end