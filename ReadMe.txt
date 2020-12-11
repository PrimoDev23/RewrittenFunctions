Benchmark for ExpressionTree-Getter/Setter (11.12.2020)

|                   Method |        Mean |     Error |    StdDev | Ratio |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------- |------------:|----------:|----------:|------:|-------:|------:|------:|----------:|
| PropertyReflectionGetter | 11,032.6 ns |  61.59 ns |  57.61 ns |  1.00 |      - |     - |     - |         - |
|                          |             |           |           |       |        |       |       |           |
|    PropertyExpTreeGetter |    180.5 ns |   0.54 ns |   0.48 ns |  1.00 |      - |     - |     - |         - |
|                          |             |           |           |       |        |       |       |           |
| PropertyReflectionSetter | 22,328.4 ns | 114.67 ns | 107.26 ns |  1.00 | 0.9766 |     - |     - |    5168 B |
|                          |             |           |           |       |        |       |       |           |
|    PropertyExpTreeSetter |  5,989.7 ns |  42.40 ns |  39.66 ns |  1.00 | 0.3738 |     - |     - |    1963 B |
|                          |             |           |           |       |        |       |       |           |
|    FieldReflectionGetter |  7,260.1 ns |  22.62 ns |  21.16 ns |  1.00 |      - |     - |     - |         - |
|                          |             |           |           |       |        |       |       |           |
|       FieldExpTreeGetter |    180.8 ns |   1.09 ns |   1.02 ns |  1.00 |      - |     - |     - |         - |
|                          |             |           |           |       |        |       |       |           |
|    FieldReflectionSetter | 14,051.3 ns | 171.52 ns | 160.44 ns |  1.00 | 0.3662 |     - |     - |    1963 B |
|                          |             |           |           |       |        |       |       |           |
|       FieldExpTreeSetter |  6,029.8 ns |  43.79 ns |  40.96 ns |  1.00 | 0.3738 |     - |     - |    1963 B |