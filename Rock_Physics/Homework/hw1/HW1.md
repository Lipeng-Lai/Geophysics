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
V = \sqrt{\frac{K_R}{\rho}}
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
V_P \approx 1.9441 km/s
$$


Code:
```python
import math

# water
vp_water = 1490
rho_water = 1
phi_water = 0.3

# Quartz
phi_quartz = 0.8 * (1 - phi_water)
vp_quartz = 6008.4
rho_quartz = 2.65
mu_quartz = 44

# Feldspar
phi_Feldspar = 0.2 * (1 - phi_water)
K_Feldspar = 75
rho_Feldspar = 2.63
mu_Feldspar = 26


# Calcalate K_water
K_water = rho_water * vp_water**2
K_water = K_water / 10**6
print("K_water: ", K_water, "GPa")

# Calculate K_quartz
vs_quartz = math.sqrt(mu_quartz / rho_quartz)
print("vs_quartz: ", vs_quartz, "km/s")
K_quartz = rho_quartz * vp_quartz**2 - 4/3 * rho_quartz * vs_quartz

K_quartz = K_quartz / 10**6
print("K_quartz: ", K_quartz, "GPa")

K = []
K.append(K_quartz)
K.append(K_Feldspar)
K.append(K_water)

phi = []
phi.append(phi_quartz)
phi.append(phi_Feldspar)
phi.append(phi_water)

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
K_water:  2.2201 GPa
vs_quartz:  4.074772826171499 km/s
K_quartz:  95.66729258646934 GPa
K:  7.0003825987688355 GPa
rho:  1.8521999999999998 g/cm^3
wood's P wave Velocity:  1.9440926053242045 km/s
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
其中$\phi = 0.3$，$V_{fl} = 1490 m/s$，对于$V_{ma}$，根据石英长石含量加权求和则有:
$$
V_{ma} = \sum_{i=1}^n \varphi_i V_i = 0.8 \times 6008.4 + 0.2 \times 6457.4
$$
则砂岩中的纵波速度为:
$$
V_{rock} \approx 3.1632 km/s
$$
Code:

```python
vp_Feldspar = math.sqrt((K_Feldspar + 4/3 * mu_Feldspar) / rho_Feldspar)
print("vp_Feldspar: ", vp_Feldspar, "km/s")
vp_quartz = vp_quartz / 1000
print("vp_quartz: ", vp_quartz, "km/s")

phi = 0.3
V_ma = 0.8 * vp_quartz + 0.2 * vp_Feldspar

print("V_ma: ", V_ma, "km/s")
V_fl = 1.49

V_rock = (1 - phi) / V_ma + phi / V_fl
V_rock = 1 / V_rock

print("V_rock: ", V_rock, "km/s")
```

Result:

```
vp_Feldspar:  6.457426139317443 km/s
vp_quartz:  6.0084 km/s
V_ma:  6.098205227863489 km/s
V_rock:  3.1632540847908897 km/s
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
print("average rho: ", rho, "g/cm^3")

# Solve for V_P
vp = (rho / 0.31)**(1/0.25)

vp = vp / 1000

print("V_P:", vp, "km/s")
```

Result:

```
average rho:  2.1521999999999997 g/cm^3
V_P: 2.323184668133828 km/s
```

### 第二问

- Gardern公式得到的结果更精确





## 问题二

![79e0052f3e0df73fab843fd1f71aa107](C:\Users\24248\Documents\Tencent Files\2424823575\nt_qq\nt_data\Pic\2023-11\Ori\79e0052f3e0df73fab843fd1f71aa107.png)

解：

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
V = \sqrt{\frac{K_R}{\rho}}
$$
由于上述数值均已得到，故代入求解得:
$$
V_P \approx 1.6417 km/s
$$

Code:
```python
phi = 0.4

sum_K = (1 - phi) / K_quartz + phi / K_water
sum_K = 1 / sum_K

rho = (1 - phi) * rho_quartz + phi * rho_water

V = math.sqrt(sum_K / rho)

print("V_P: ", V, "km/s")
```

Result：
```
V_P:  1.6417215104007534 km/s
```



## 问题三

![0f0e0c2a4ec0b9953e7a2ecd675d5427](C:\Users\24248\Documents\Tencent Files\2424823575\nt_qq\nt_data\Pic\2023-11\Ori\0f0e0c2a4ec0b9953e7a2ecd675d5427.png)

### Voigt模型上限

- Voigt模型为并联模型，纵的体积模量为各矿物的弹性模量以其体积的百分比的累加

$$
K_V = \sum_{i=1}^N K_i f_i = 0.9 \times 37 + 0.1 \times 76.8 = 40.98GPa
$$

$$
\mu_V = \sum_{i=1}^N \mu_i f_i = 0.9 \times 44 + 0.1 \times 32 = 42.8GPa
$$


$$
V_P^{Voigt} = \sqrt{\frac{K_V + \frac{4}{3}\mu_V}{\rho}} = 6.075km/s
$$

$$
V_S^{Voigt} = \sqrt{\frac{\mu_V}{\rho}} = 4.014 km/s
$$



### Reuss模型下限

- Reuss模型为串联模型

$$
K_R^{-1} = \sum_{i=1}^N K_i^{-1} f_i = \frac{0.9}{37} + \frac{0.1}{76.8} \\
K_R = 42.8 GPa
$$


$$
\mu_R^{-1} = \sum_{i=1}^N \mu_i^{-1} f_i = \frac{0.9}{44} + \frac{0.1}{32} \\
\mu_R = 42.41Gpa
$$

$$
V_P^{Reuss} = \sqrt{\frac{K_R + \frac{4}{3}\mu_R}{\rho}} = 5.998km/s 
$$

$$
V_S^{Reuss} = \sqrt{\frac{\mu_R}{\rho}} = 3.996km/s
$$


### Hashin-Shtrikman模型

- 当坚硬材料组成外壳时所得的是上限；当坚硬材料组成内部球核时所得的时下限，分别对应"坚硬孔隙形态"和"柔软孔隙形态"


$$
K_{bnd} = K_1 + \frac{\phi_2}{1 / (K_2 - K_1) + \phi_1 / (K_1 + \frac{4}{3}\mu_1)}
$$

$$
\mu_{bnd} = \mu_1 + \frac{\phi_2}{1/(\mu_2 - \mu_1) + \frac{2\phi_1(K_1 + 2\mu_1)}{5\mu_1 (K_1 + \frac{4}{3}\mu_1)}}
$$

- 其中，将$K_1, K_2$互换可分别得到上/下界限，一般来说，当坚硬材料定义为材料1，求得的是上限，当柔韧材料定义为材料1时求得的是下限

$$
K_{up} = 72.683 GPa \\
\mu_{up} = 33.179 GPa \\


\\
K_{down} = 40.791 GPa \\
\mu_{down} = 33.177 GPa
$$

- 使用Hashin-Shtrikman上下限估算

$$
V_P^{up} = \sqrt{\frac{K_{up} + \frac{4}{3}\mu_{up}}{\rho}} = 6.635km/s
$$

$$
V_S^{up} = \sqrt{\frac{\mu_{up}}{\rho}} = 3.534km/s
$$

$$
V_P^{down} = \sqrt{\frac{K_{down} + \frac{4}{3}\mu_{down}}{\rho}} = 5.658km/s
$$

$$
V_S^{down} = \sqrt{\frac{\mu_{down}}{\rho}} = 3.534km/s
$$

Code:

```python
phi = 0.1

# shiying
K_shiying = 37
mu_shiying = 44
rho_shiying = 2650 / 1000

# fangjieshi
K_fang = 76.8
mu_fang = 32
rho_fang = 2710 / 1000

# rho
rho = (1 - phi) * rho_shiying + phi * rho_fang
print("rho: ", rho)

# Voigt
Kv = K_shiying * (1 - phi) + K_fang * phi
print("Kv: ", Kv)

muv = mu_shiying * (1 - phi) + mu_fang * phi
print("muv: ", muv)

Vp_voigt = math.sqrt((Kv + 4/3*muv) / rho)
print("Vp_voigt: ", Vp_voigt)

Vs_voigt = math.sqrt(muv / rho)
print("Vs_voigt: ", Vs_voigt)

# Reuss
Kr = (1 - phi) / K_shiying + phi / K_fang
Kr = 1 / Kr
print("Kr: ", Kr)

mur = (1 - phi) / mu_shiying + phi / mu_fang
mur = 1 / mur
print("mur: ", mur)

Vp_reuss = math.sqrt((Kr + 4/3*mur) / rho)
print("Vp_reuss: ", Vp_reuss)

Vs_reuss = math.sqrt(mur / rho)
print("Vs_reuss: ", Vs_reuss)
```

Result:

```python
rho:  2.6559999999999997
Kv:  40.980000000000004
muv:  42.800000000000004
Vp_voigt:  6.075784775859847
Vs_voigt:  4.014281732928732
Kr:  39.02224663553969
mur:  42.40963855421687
Vp_reuss:  5.998507324821699
Vs_reuss:  3.9959334823559036
```

Code:

```python
def hashin_shtrikman_upper_bound(phi, K1, mu1, K2, mu2):
    K_bnd = K1 + phi / (1 / (K2 - K1) + phi / (K1 + 4/3 * mu1))
    mu_bnd = mu1 + phi / (1 / (mu2 - mu1) + (2 * phi * (K1 + 2 * mu1)) / (5 * mu1 * (K1 + 4/3 * mu1)))
    return K_bnd, mu_bnd

def hashin_shtrikman_lower_bound(phi, K1, mu1, K2, mu2):
    K_bnd = K1 + phi / (1 / (K2 - K1) + phi / (K1 + 4/3 * mu1))
    mu_bnd = mu1 + phi / (1 / (mu2 - mu1) + (2 * phi * (K1 + 2 * mu1)) / (5 * mu1 * (K1 + 4/3 * mu1)))
    return K_bnd, mu_bnd


K1_rock = 76.8e9  # 方解石体积模量，Pa
mu1_rock = 32e9   # 方解石剪切模量，Pa
rho_rock = 2710   # 方解石密度，kg/m^3

K1_quartz = 37e9  # 石英体积模量，Pa
mu1_quartz = 44e9 # 石英剪切模量，Pa
rho_quartz = 2650 # 石英密度，kg/m^3

# 方解石含量为10%
phi = 0.1

# 计算Hashin-Shtrikman上、下界
K_upper, mu_upper = hashin_shtrikman_upper_bound(phi, K1_rock, mu1_rock, K1_quartz, mu1_quartz)
# K_lower, mu_lower = hashin_shtrikman_lower_bound(phi, K1_rock, mu1_rock, K1_quartz, mu1_quartz)
K_lower, mu_lower = hashin_shtrikman_lower_bound(phi, K1_quartz, mu1_rock, K1_rock, mu1_quartz)

print("Hashin-Shtrikman上界：")
print("体积模量 (K_bnd) =", K_upper / 1e9, "GPa")
print("剪切模量 (mu_bnd) =", mu_upper / 1e9, "GPa")

print("\nHashin-Shtrikman下界：")
print("体积模量 (K_bnd) =", K_lower / 1e9, "GPa")
print("剪切模量 (mu_bnd) =", mu_lower / 1e9, "GPa")

Vp_up = math.sqrt((K_upper/1e9 + 4/3*mu_upper/1e9) / rho)
print("Vp_up: ", Vp_up)

Vs_up = math.sqrt(mu_upper/1e9 / rho)
print("Vs_up: ", Vs_up)

Vp_down = math.sqrt((K_lower/1e9 + 4/3*mu_lower/1e9) / rho)
print("Vp_down: ", Vp_down)

Vs_down = math.sqrt(mu_lower/1e9 / rho)
print("Vs_up: ", Vs_down)
```

Result:

```
Hashin-Shtrikman上界：
体积模量 (K_bnd) = 72.68283784563874 GPa
剪切模量 (mu_bnd) = 33.17915423758554 GPa

Hashin-Shtrikman下界：
体积模量 (K_bnd) = 40.79062724157169 GPa
剪切模量 (mu_bnd) = 33.177605781272455 GPa
Vp_up:  6.634887205054997
Vs_up:  3.5344237783370116
Vp_down:  5.6580333572818455
Vs_up:  3.5343413023849504
```

