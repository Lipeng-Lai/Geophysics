![2e0dd1642c72dfbc11872353abde1493](C:\Users\24248\Documents\Tencent Files\2424823575\nt_qq\nt_data\Pic\2023-11\Ori\2e0dd1642c72dfbc11872353abde1493.png)

## 问题一

### 一、Wood's方程

在一个流体悬浮或流体混合物中，若其非均匀性比波长小，则声波速度可由Wood方程精确地给定，对于混合物的等应力平均$K_R$，即:
$$
\frac{1}{K_R} = \sum_{i=1}^n \frac{\varphi_i}{K_i}
$$
对于平均密度$\rho$，定义为:
$$
\rho = \sum_{i=1}^n \varphi_i \rho_i
$$
则根据体积模量与速度和密度的关系可得:
$$
v = \sqrt{\frac{K_R}{\rho}}
$$
由已知项根据弹性模型参数计算可推出石英的体积模量
$$
K = \rho V_P^2 - \frac{4}{3}\rho V_S^2
$$
其中，对于横波速度$V_S$:
$$
V_S = \sqrt{\frac{\mu}{\rho}}
$$
故可得砂岩的纵波速度为:
$$
V_P = 5.8538 km/s
$$


Code:
```python
import math

# Quartz
phi_quartz = 0.8
vp_quartz = 6008.4
rho_quartz = 2.65
mu_quartz = 44

# Feldspar
phi_Feldspar = 0.2
K_Feldspar = 75
rho_Feldspar = 2.63
mu_Feldspar = 26

# Calculate K_quartz
vs_quartz = math.sqrt(mu_quartz / rho_quartz)
K_quartz = rho_quartz * vp_quartz**2 - 4/3 * rho_quartz * vs_quartz

K_quartz = K_quartz / 10**6
print("K_quartz: ", K_quartz, "GPa")

K = []
K.append(K_quartz)
K.append(K_Feldspar)

phi = []
phi.append(phi_quartz)
phi.append(phi_Feldspar)

# Calculate average K
res_K = 0
for ki, phii in zip(K, phi):
    res_K = res_K + phii / ki

res_K = 1 / res_K


print("K: ", res_K, "GPa")

# Calculate average rho 
Rho = []
Rho.append(rho_quartz)
Rho.append(rho_Feldspar)

rho = 0
for phii, rhoi in zip(phi, Rho):
    rho = rho + phii * rhoi

print("rho: ", rho, "g/cm^3")

# Calculate wood's Velocity
Velocity_wood = math.sqrt(res_K / rho)

print("wood's P wave Velocity: ", Velocity_wood, "km/s")
```

Result:

```
K_quartz:  95.66729258646934 GPa
K:  90.67020547847233 GPa
rho:  2.646 g/cm^3
wood's P wave Velocity:  5.853793256898434 km/s
```



### 二、Wyllie时间平均方程

- 依照孔隙度的计算方法，把岩石中的孔隙全都集中为一层，其厚度为孔隙度$\phi L$，岩石的其余固体部分为另一层，厚度则为$(1-\phi)L$

- 弹性波经过岩石的总时间$\Delta t$分为两部分，经过固体骨架的时间$\Delta t_{ma}$和经过孔隙的时间$\Delta t_{fl}$

$$
\Delta t = \Delta t_{ma} + \Delta t_{fl}
$$

则可转化为:
$$
\frac{1}{V_{rock}} = \frac{1 - \phi}{V_{ma}} + \frac{\phi}{V_{fl}}
$$
其中$\phi = 0.3$，$V_{ma} = 5.8537km/s$，即使用wood's模型估计的速度，$V_{fl}$为$P$波在水中的传播速度$V_{fl} = 1.49 km /s$

则砂岩中的纵波速度为:
$$
V_{rock} = 3.116 km/s
$$
Code:

```python
phi = 0.3
V_ma = 5.8537
V_fl = 1.49

V_rock = (1 - phi) / V_ma + phi / V_fl
V_rock = 1 / V_rock

print("V_rock: ", V_rock)
```

Result:

```
V_rock:  3.1159950841517485
```



### Gardern公式

- Gardner等人提出了使用地震方法区分气饱和砂岩和水饱砂岩的岩石物理原理，由此给出了速度与密度之间的平均变换公式:

$$
\rho = 0.31 V_P^{0.25}
$$

其中$V_P$是纵波速度，单位$m/s$，$rho$是体积密度($g/cm^3$)

则根据平均密度估算：
$$
\rho = (1 - \phi)(\varphi_{石英} \times \rho_{石英} + \varphi_{长石} \times \rho_{长石}) + \phi \rho_{水}
\\
= 2.1522 g / cm^3
$$
代入上式可得:
$$
V_P \approx 2.323 km/s
$$
Code:

```python
phi = 0.3
varphi1 = 0.8
varphi2 = 0.2
rho_water = 1

rho = (1 - phi) * (varphi1 * rho_quartz + varphi2 * rho_Feldspar) + phi * rho_water
print("average rho: ", rho)

# Solve for V_P
vp = (rho / 0.31)**(1/0.25)

print("V_P:", vp)
```

Result:

```
average rho:  2.1521999999999997
V_P: 2323.1846681338284
```



## 问题二

- Wyllie时间平均方程得到的结果更精确
