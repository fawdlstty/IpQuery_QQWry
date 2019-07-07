# IpQuery_QQWry

纯真IP数据库查询（C#）

数据库文件 `qqwry.dat` 下载地址：[qqwry.dat](https://github.com/WisdomFusion/qqwry.dat)

这个数据库使用GB2312编码方式进行编码。默认 .Net Core 不支持此编码，所以 .Net Framework 可以直接使用；.Net Core 需要按如下步骤：

1. 工具 -> NuGet 包管理器 -> 管理解决方案的 NuGet 程序包;
2. 浏览 -> System.Text.Encoding.CodePages -> 安装
3. 加入初始化代码：`Encoding.RegisterProvider (CodePagesEncodingProvider.Instance);`

使用方式：

```csharp
IpQuery_QQWry _ip_query = new IpQuery_QQWry ("qqwry.dat");
var (_info1, _info2) = _ip_query.find_info ("8.8.8.8");
Console.WriteLine (_info1);
Console.WriteLine (_info2);
```

开源方式：跟随 qqwry.dat 项目
