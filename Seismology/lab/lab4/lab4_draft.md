## 水平层状介质时距曲线

$$
x(p)=2\sum_{j=0}^nx_j=2\sum_{j=0}^nh_j\tan i_j \tag{3.3-7}
$$

$$
T(p)=2\sum_{j=0}^n\Delta T_j=2\sum_{j=0}^n\frac{h_j}{v_j\cos i_j} \tag{3.3-8}
$$

其中$p$为射线参数，可由Snell's定律可求：
$$
p = \frac{\sin i_0}{v_0} = \frac{\sin i_j}{v_j}
$$

## 双曲线近似方程

$$
T(x)_{n+1}^2 = \frac{x^2}{\bar{V}_n^2} + t_n^2 \tag{3.3-11}
$$

对于总时间$t_n$有:
$$
t_{n}=2\sum_{j=0}^{n}\Delta t_{j}=2\sum_{j=0}^{n}(h_{j}/v_{j})
$$
对于均方根速度$\bar{V}$有：
$$
\bar{V}_{n}^{2}=\left(\sum_{j=0}^{n}v_{j}^{2}\Delta t_{j}\right)/\left(\sum_{j=0}^{n}\Delta t_{j}\right)
$$

## 思路流程

利用多层水平层状介质中的时距曲线表达式（方程3.3-7和方程3.3-8）和其双曲线近似表达式（方程3.3-11），在MATLAB中编写程序计算并绘制走时曲线。（20分） 

其中已知参数为:

height = [40, 50, 60, 80]; % 地层厚度 单位km

velocity = [6.3, 6.8, 7.5, 8.2]; % 层速度 单位km/s

angle = 0: 2: 50 % 初始入射角度

offset = 0: 1: 1000; % 偏移距 单位 km


$$
\theta_2 = \arcsin \frac{v_2 \sin \theta_1}{v_1}
$$
