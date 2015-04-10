#简述Qi命名空间下的基础功能

# 介绍 #

做了很多年开发，因此会有很多代码片段，集合之后就成为这个叫做Qi.dll的项目，有些代码
来自网络，大部分是我自己写的。来自于网络的代码，我已经在源码中列出了出处，感谢这些无私的程序员。


# 基本功能 #

## ApplicationHelper ##

MapPath 获得路径，这个支持Web和WinForm两种，并且并不依赖System.Web.Dll，因此可以
使用Profile部署

```
ApplicationHelper.MapPath("~/Config/hibernate.cfg.config");
```