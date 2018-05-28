``` ini

BenchmarkDotNet=v0.10.14, OS=Windows 7 SP1 (6.1.7601.0)
Intel Core i5-5200U CPU 2.20GHz (Broadwell), 1 CPU, 4 logical and 2 physical cores
Frequency=2143496 Hz, Resolution=466.5276 ns, Timer=TSC
.NET Core SDK=2.1.101
  [Host]     : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT  [AttachedDebugger]
  Job-DSNKYI : .NET Core 2.0.6 (CoreCLR 4.6.26212.01, CoreFX 4.6.26212.01), 64bit RyuJIT

Runtime=Core  LaunchCount=1  RunStrategy=ColdStart  
TargetCount=1  UnrollFactor=1  WarmupCount=1  

```
| Method |      Mean | Error |
|------- |----------:|------:|
|   Copy |   1.075 s |    NA |
| Insert | 146.759 s |    NA |
