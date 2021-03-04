# IpQuery_QQWry

纯真IP数据库查询（C++、C#）

数据库仓库：

- [out0fmemory/qqwry.dat](https://github.com/out0fmemory/qqwry.dat)：每天更新，仓库很大
- [WisdomFusion/qqwry.dat](https://github.com/WisdomFusion/qqwry.dat)：更新较慢，仓库较小，下载方便

## C++

代码里面有用到部分C++11语法，如果需要在较老编译环境下使用，那么需要做简单修改。另外读到的字符串编码是GB2312，如果后续需要UTF8之类的编码，需要自己手工处理。

使用方式：

```cpp
IpQuery_QQWry _ip_query ("qqwry.dat");
auto [_info0, _info1, _desp] = _ip_query.find_info ("8.8.8.8");
std::cout << "info0[" << _info0 << "]\n";
std::cout << "info1[" << _info1 << "]\n";
std::cout << "_desp[" << _desp << "]\n";
```

## C\#

这个数据库使用GB2312编码方式进行编码。默认 .Net Core 不支持此编码，所以 .Net Framework 可以直接使用；.Net Core 需要按如下步骤：

1. 工具 -> NuGet 包管理器 -> 管理解决方案的 NuGet 程序包;
2. 浏览 -> System.Text.Encoding.CodePages -> 安装
3. 加入初始化代码：`Encoding.RegisterProvider (CodePagesEncodingProvider.Instance);`

使用方式：

```csharp
IpQuery_QQWry _ip_query = new IpQuery_QQWry ("qqwry.dat");
var (_info0, _info1, _desp) = _ip_query.find_info ("8.8.8.8");
Console.WriteLine ($"info0[{_info0}]");
Console.WriteLine ($"info1[{_info1}]");
Console.WriteLine ($"desp[{_desp}]");
```

## 其他推荐

[ip2region](https://github.com/lionsoul2014/ip2region)

## 开源协议

MIT
