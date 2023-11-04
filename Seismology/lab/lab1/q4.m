clear, clc, close all;

% (1)
t1 = 0: 0.01 : 10;
x1 = 1;
w1 = 1;
k1 = 1;
subplot(2, 2, 1);
cal_cos(t1, x1, w1, k1);
title("t=0~10 x=1 w=1 k=1");

% (2)
t2 = 0: 0.01 : 10;
x2 = 0;
w2 = 4;
k2 = 1;
subplot(2, 2, 2);
cal_cos(t2, x2, w2, k2);
title("t=0~10 x=0 w=4 k=1");

% (3)
t3 = 0;
x3 = 0: 0.01 : 10;
w3 = 1;
k3 = 2;
subplot(2, 2, 3);
cal_cos(t3, x3, w3, k3);
title("x=0~10 t=0 w=1 k=2");

% (4)
t4 = 0;
x4 = 0: 0.01 : 10;
w4 = 1;
k4 = 4;
subplot(2, 2, 4);
cal_cos(t4, x4, w4, k4);
title("x=0~10 t=0 w=1 k=4");