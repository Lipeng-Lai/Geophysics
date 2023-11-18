function [t, amp] = Make_Reflector_Series(v, rho, h, t0, tlen, dt)

t = t0: dt: tlen;
nLayers = length(h);
amp = zeros(size(t));
t_sum = 0; % 总走时
amp0 = 1;

for i = 1: nLayers
    % 获取每个反射界面的走时
    t_sum = t_sum + 2*h(i)/v(i);
    
    % 计算反射系数和透射系数
    [r1, t1] = Refle_Trans_Coeff(v(i), rho(i), v(i+1), rho(i+1));
    [~, t2] = Refle_Trans_Coeff(v(i+1), rho(i+1), v(i), rho(i));
    
    % 时间单位个数
    k = floor(t_sum/dt);
    if (k < length(t))
        amp(k) = amp0 * r1;
    end

    amp0 = amp0*t1*t2;
end

end