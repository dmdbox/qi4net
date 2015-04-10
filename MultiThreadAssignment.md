# Introduction #

自動地把資源平均分配到不同的thread執行。


# Details #

## 應用場景 ##

> 假設現在有多個Db需要訪問，假設想多個thread去執行，那麼可以這樣寫

```

   var executeThreadCount=4;
   var assignment=new MultiThreadAssignment(2,accessDBMethod);

   var dbConnections=new []{conn1，conn2,conn3,conn4};
   assignment.Execute(dbConnections); //同步執行

```