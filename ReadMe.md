## Rewritten Functions

This library is for everyone who cares about micro-optimisations and also wants some extensions for other .NET classes.
It contains different optimizations (and extensions), for example:

 - String helpers (e.g. ContainsIgnoreCase)
 - Math helpers (e.g. Faculty)
 - Expression Trees for replacing reflection (Get/Set Properties/Fields and execute Methods)

## Benchmark results

**String helpers**

|                    Method |         Mean |     Error |    StdDev | Ratio |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------------------- |-------------:|----------:|----------:|------:|-------:|------:|------:|----------:|
|         DefaultStartsWith |  9,072.83 ns | 46.243 ns | 43.256 ns |  1.00 |      - |     - |     - |         - |
|                           |              |           |           |       |        |       |       |           |
|              RFStartsWith |    780.17 ns |  3.832 ns |  3.584 ns |  1.00 |      - |     - |     - |         - |
|                           |              |           |           |       |        |       |       |           |
|    RFStartsWithIgnoreCase |  1,762.30 ns |  3.537 ns |  3.136 ns |  1.00 |      - |     - |     - |         - |
|                           |              |           |           |       |        |       |       |           |
|           DefaultEndsWith |  9,541.02 ns | 35.392 ns | 33.105 ns |  1.00 |      - |     - |     - |         - |
|                           |              |           |           |       |        |       |       |           |
|                RFEndsWith |  1,034.91 ns |  1.961 ns |  1.739 ns |  1.00 |      - |     - |     - |         - |
|                           |              |           |           |       |        |       |       |           |
|      RFEndsWithIgnoreCase |  2,473.15 ns |  6.717 ns |  5.954 ns |  1.00 |      - |     - |     - |         - |
|                           |              |           |           |       |        |       |       |           |
|          IsNotNullOrEmpty |     82.79 ns |  0.108 ns |  0.101 ns |  1.00 |      - |     - |     - |         - |
|                           |              |           |           |       |        |       |       |           |
|                RFIsFilled |     81.45 ns |  0.297 ns |  0.278 ns |  1.00 |      - |     - |     - |         - |
|                           |              |           |           |       |        |       |       |           |
| DefaultContainsIgnoreCase | 25,485.69 ns | 95.362 ns | 84.536 ns |  1.00 | 2.6550 |     - |     - |   14021 B |
|                           |              |           |           |       |        |       |       |           |
|      RFContainsIgnoreCase | 21,296.74 ns | 78.993 ns | 73.890 ns |  1.00 |      - |     - |     - |         - |

----
**Math helpers**

|            Method |       Mean |     Error |    StdDev | Ratio |   Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------ |-----------:|----------:|----------:|------:|--------:|------:|------:|----------:|
|         PowNormal |   3.435 us | 0.0045 us | 0.0040 us |  1.00 |       - |     - |     - |         - |
|                   |            |           |           |       |         |       |       |           |
|             PowRF |   2.792 us | 0.0240 us | 0.0224 us |  1.00 |       - |     - |     - |         - |
|                   |            |           |           |       |         |       |       |           |
| TryParseIntNormal |   9.212 us | 0.0369 us | 0.0345 us |  1.00 |       - |     - |     - |         - |
|                   |            |           |           |       |         |       |       |           |
|     tryParseIntRF |   2.776 us | 0.0106 us | 0.0094 us |  1.00 |       - |     - |     - |         - |
|                   |            |           |           |       |         |       |       |           |
|     BigIntVariant | 339.219 us | 1.4480 us | 1.3544 us |  1.00 | 20.9961 |     - |     - |  110165 B |
|                   |            |           |           |       |         |       |       |           |
|      RFModLongInt |  17.333 us | 0.0564 us | 0.0471 us |  1.00 |       - |     - |     - |         - |

---
**Expression Trees**
|                   Method |        Mean |     Error |   StdDev | Ratio |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------- |------------:|----------:|---------:|------:|-------:|------:|------:|----------:|
| PropertyReflectionGetter | 10,945.4 ns |   9.41 ns |  7.35 ns |  1.00 |      - |     - |     - |         - |
|                          |             |           |          |       |        |       |       |           |
|    PropertyExpTreeGetter |    226.8 ns |   0.62 ns |  0.58 ns |  1.00 |      - |     - |     - |         - |
|                          |             |           |          |       |        |       |       |           |
| PropertyReflectionSetter | 22,421.7 ns |  36.36 ns | 30.37 ns |  1.00 | 0.9766 |     - |     - |    5168 B |
|                          |             |           |          |       |        |       |       |           |
|    PropertyExpTreeSetter |  6,015.9 ns |  23.44 ns | 21.92 ns |  1.00 | 0.3738 |     - |     - |    1963 B |
|                          |             |           |          |       |        |       |       |           |
|    FieldReflectionGetter |  7,126.0 ns |  13.06 ns | 12.21 ns |  1.00 |      - |     - |     - |         - |
|                          |             |           |          |       |        |       |       |           |
|       FieldExpTreeGetter |    227.0 ns |   0.37 ns |  0.35 ns |  1.00 |      - |     - |     - |         - |
|                          |             |           |          |       |        |       |       |           |
|    FieldReflectionSetter | 14,085.5 ns |  32.80 ns | 30.68 ns |  1.00 | 0.3662 |     - |     - |    1963 B |
|                          |             |           |          |       |        |       |       |           |
|       FieldExpTreeSetter |  5,981.2 ns |  18.17 ns | 17.00 ns |  1.00 | 0.3738 |     - |     - |    1963 B |
|                          |             |           |          |       |        |       |       |           |
|         MethodReflection | 35,230.1 ns | 101.97 ns | 95.38 ns |  1.00 | 1.4038 |     - |     - |    7588 B |
|                          |             |           |          |       |        |       |       |           |
|            MethodExpTree |  8,467.0 ns |  26.22 ns | 23.24 ns |  1.00 | 0.8240 |     - |     - |    4383 B |
