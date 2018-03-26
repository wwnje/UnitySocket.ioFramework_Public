using System;
using System.Collections.Generic;

[Serializable]
public class ServerInfo
{
    public int vision;
    public string announcement;
    public List<ServerData> datas = new List<ServerData>();
}

[Serializable]
public class ServerData
{
    public int id; // 服务器ID
    public string name; // 服务器名
    public string host; // 服务器地址
    public int port; // 服务器端口
    public int def; // 默认标志(唯一），只能有一个，0无，1推荐
    public int recommend; // 推荐标志,0无，1推荐，2新服
}