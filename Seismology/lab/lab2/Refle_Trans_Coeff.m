function [Refle_Coeff, Trans_Coeff, Refle_Energy_ratio, Trans_Energy_ratio] = Refle_Trans_Coeff(alpha1, ...
    beta1, rho1, alpha2, beta2, rho2, wave_type, travel_type)

if wave_type == 0 && travel_type == 0
    Refle_Coeff = (rho1 * alpha1 - rho2 * alpha2) / (rho1 * alpha1 + rho2 * alpha2);
    Trans_Coeff = 2 *  rho1 * alpha1 / (rho1 * alpha1 + rho2 * alpha2);
    Refle_Energy_ratio = Refle_Coeff^2;
    Trans_Energy_ratio = Trans_Coeff^2 * rho2 * alpha2 / (rho1 * alpha1);
    disp("%%%%%%%%%%%%%%%%%%%%%%%%%%");
    disp("Downgoing P-wave indident case");
    disp("%%%%%%%%%%%%%%%%%%%%%%%%%%");
    fprintf("Reflection coeffient: %f\n", Refle_Coeff);
    fprintf("Transmission coeffient: %f\n", Trans_Coeff);
    fprintf("Reflection energy flux ratio: %f\n", Refle_Energy_ratio);
    fprintf("Transmission energy flux ratio: %f\n", Trans_Energy_ratio);
elseif wave_type == 0 && travel_type ==1
    Refle_Coeff = (rho2 * alpha2 - rho1 * alpha1) / (rho1 * alpha1 + rho2 * alpha2);
    Trans_Coeff = 2 * rho2 * alpha2 / (rho1 * alpha1 + rho2 * alpha2);
    Refle_Energy_ratio = Refle_Coeff^2;
    Trans_Energy_ratio = Trans_Coeff^2 * rho1 * alpha1 / (rho2 * alpha2);
    disp("%%%%%%%%%%%%%%%%%%%%%%%%%%");
    disp("Upgoing P-wave indident case");
    disp("%%%%%%%%%%%%%%%%%%%%%%%%%%");
    fprintf("Reflection coeffient: %f\n", Refle_Coeff);
    fprintf("Transmission coeffient: %f\n", Trans_Coeff);
    fprintf("Reflection energy flux ratio: %f\n", Refle_Energy_ratio);
    fprintf("Transmission energy flux ratio: %f\n", Trans_Energy_ratio);
elseif wave_type == 1 && travel_type == 0
    Refle_Coeff = (rho1 * beta1 - rho2 * beta2) / (rho1 * beta1 + rho2 * beta2);
    Trans_Coeff = 2 * rho1 * beta1 / (rho1 * beta1 + rho2 * beta2);
    Refle_Energy_ratio = Refle_Coeff^2;
    Trans_Energy_ratio = Trans_Coeff^2 * rho2 * beta2 / (rho1 * beta1);
    disp("%%%%%%%%%%%%%%%%%%%%%%%%%%");
    disp("Downgoing S-wave indident case");
    disp("%%%%%%%%%%%%%%%%%%%%%%%%%%");
    fprintf("Reflection coeffient: %f\n", Refle_Coeff);
    fprintf("Transmission coeffient: %f\n", Trans_Coeff);
    fprintf("Reflection energy flux ratio: %f\n", Refle_Energy_ratio);
    fprintf("Transmission energy flux ratio: %f\n", Trans_Energy_ratio);
elseif wave_type == 1 && travel_type == 1
    Refle_Coeff = (rho2 * beta2 - rho1 * beta1) / (rho1 * beta1 + rho2 * beta2);
    Trans_Coeff = 2 * rho2 * beta2 / (rho1 * beta1 + rho2 * beta2);
    Refle_Energy_ratio = Refle_Coeff^2;
    Trans_Energy_ratio = Trans_Coeff^2 * rho1 * beta1 / (rho2 * beta2);
    disp("%%%%%%%%%%%%%%%%%%%%%%%%%%");
    disp("Upgoing S-wave indident case");
    disp("%%%%%%%%%%%%%%%%%%%%%%%%%%");
    fprintf("Reflection coeffient: %f\n", Refle_Coeff);
    fprintf("Transmission coeffient: %f\n", Trans_Coeff);
    fprintf("Reflection energy flux ratio: %f\n", Refle_Energy_ratio);
    fprintf("Transmission energy flux ratio: %f\n", Trans_Energy_ratio);
end


