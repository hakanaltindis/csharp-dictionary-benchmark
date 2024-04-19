// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using DictionaryBenckmark;

_ = BenchmarkRunner.Run<DictionaryBenchmarking>();
