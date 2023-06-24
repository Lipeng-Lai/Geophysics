## 6.1 $z$变换定义及收敛域

对连续信号进行均匀冲激取样后，就得到离散信号
$$
f_s(t) = f(t) \delta_T(t) = \sum_{k=-\infty}^{+\infty} f(kT)\delta(t-kT)
$$
上式两端取双边拉普拉斯变换，得
$$
F_{sb}(s) = \sum_{k=-\infty}^{+\infty} f(kT)e^{-kTs}
$$
令$z=e^{sT}$，即有
$$
F(z) = \sum_{k=-\infty}^{+\infty} f(k)z^{-k}, 双边z变换 \\
F(z) = \sum_{k=0}^{+\infty} f(k)z^{-k}, 单边z变换
$$
特别的，如果$f(k)$为因果序列，则单边，双边$z$变换相等，统称单边双边$z$变换均为$z$变换

即
$$
f(k) \leftrightarrow F(z)
$$

### 收敛域

当幂级数收敛时，$z$变换才存在，即满足绝对可积条件：
$$
\sum_{k=-\infty}^{+\infty} |f(k)z^{-k}| < \infty
$$
同样的，它也是序列$f(k)$的$z$变换存在的`充分条件`

例题：

![image-20230624093348473](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624093348473.png)

![image-20230624093803723](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624093803723.png)

### 离散序列的收敛域情况分类

![image-20230624094429402](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624094429402.png)

特别的，对于双边$z$变换必须表明收敛域，

对于单边$z$变换，其收敛域是某个圆外区域，可省略



### 常用序列的$z$变换

![image-20230624094555470](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624094555470.png)



## 6.2 $z$变换性质

1. 线性

$$
f_1(k) \leftrightarrow F_1(z), \alpha_1 < |z| < \beta_1 \\
f_2(k) \leftrightarrow F_2(z), \alpha_2 < |z| < \beta_2
\\
a_1f_1(t) + a_2f_2(t) \leftrightarrow a_1F_1(z) + a_2F_2(z), \\
max(\alpha_1, \alpha_2) < |z| < min(\beta_1, \beta_2)
$$

合成后，收敛域至少是$F_1(z),F_2(z)$收敛域的交集



2. 移位特性

- 双边$z$变换的移位

$$
f(k \pm m) \leftrightarrow z^{\pm}F(z)
$$

- 单边$z$变换的移位

$$
f(k-m) \leftrightarrow z^{-m}F(z) + \sum_{k=0}^{m-1} f(k-m)z^{-k} \\
f(k+m) \leftrightarrow z^{m}F(z) - \sum_{k=0}^{m-1}f(k)z^{m-k}
$$

即移位之后的部分 = 已经在单边的部分 + 之前在负半轴的部分

特别的，若$f(k)$为因果序列
$$
f(k-m) \leftrightarrow z^{-m}F(z) \\
f(k-m)\epsilon(k-m) \leftrightarrow z^{-m}F(z)
$$


3. $k$域反转，仅适用双边$z$变换

$$
f(k) \leftrightarrow F(z), \alpha < |z| < \beta \\
f(-k) \leftrightarrow F(z^{-1}), \frac{1}{\beta} < |z| < \frac{1}{\alpha}
$$

例题：

![image-20230624095912011](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624095912011.png)

![image-20230624100100041](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624100100041.png)



4. $z$域尺度变换

$$
f(k) \leftrightarrow F(z), \alpha < |z| < \beta \\
a^kf(k) \leftrightarrow F(\frac{z}{a}), |a|\alpha < |z| < |a|\beta
$$



5. 序列乘$k$($z$域微分)

$$
f(k) \leftrightarrow F(z), \alpha < |z| < \beta \\
kf(k) \leftrightarrow (-z)\frac{d}{dz}F(z)
$$

例题：

![image-20230624100518265](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624100518265.png)

![image-20230624100530201](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624100530201.png)

![image-20230624100537140](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624100537140.png)



6. 时域卷积

$$
f_1(k) * f_2(k) \leftrightarrow F_1(z) F_2(z)
$$

收敛域取交集



7. 部分和

$$
f(k) \leftrightarrow F(z), \alpha < |z| < \beta \\
\sum_{i=-\infty}^k f(i) \leftrightarrow \frac{z}{z-1}F(z), max(\alpha, 1) < |z| < \beta
$$

例题：
$$
f(k) * \epsilon(k) = \sum_{i=-\infty}^{\infty}f(i)\epsilon(k-i) \\
= \sum_{i=-\infty}^k f(i)\epsilon(k-i) \\由部分和性质可得 \\
= \frac{z}{z-1}F(z)
$$


## 6.3 初值定理和终值定理

### 初值定理

由象函数直接求序列的出自$f(M)$，
$$
f(M) = \lim_{z\rightarrow \infty} z^m F(z)
$$
特别的，对于因果序列有：
$$
f(0) = \lim_{z \rightarrow \infty} F(z)
$$

### 终值定理

如果序列存在终值，即
$$
f(\infty) = \lim_{k \rightarrow \infty} f(k)
$$
则序列的终值
$$
f(\infty) = \lim_{z\rightarrow 1} (z-1)F(z)
$$


## 6.4 逆$z$变换：幂级数和部分分式展开

$F(z)$的逆$z$变换
$$
f(k) = \frac{1}{2\pi j} \oint_c F(z)z^{k-1} dz, -\infty < k < \infty
$$
应用：`L10 Z变换`



## 6.5 差分方程的$z$变换解

一样的方法，就是注意序列移序后变换

`L11-Z变换P25-P34`



## 6.6 系统函数$H(z)$

一样的方法
$$
H(z) = \frac{Y_{zs}(z)}{F(z)}
$$
`L11-Z变换P35-P38`





## 6.7 系统函数与系统特性

![image-20230624110022060](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624110022060.png)

### 结论：

1. $H(z)$的极点在单位圆内，对应$h(k)$按指数规律衰减
2. 极点在单位圆上，一阶极点对应$h(k)$为稳态分量
3. 极点在单位圆外，对应$h(k)$按指数规律增长



## 6.8 离散系统稳定性判据

![image-20230624110230220](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624110230220.png)

![image-20230624110425385](C:\Users\24248\AppData\Roaming\Typora\typora-user-images\image-20230624110425385.png)

阶跃响应
$$
Y(z) = F(z) H(z)
$$
