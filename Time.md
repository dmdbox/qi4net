# Introduction #

添加Time對象和一些常用算法，Time是一個Struct


# Details #
其他用法，請參考 Qi.Test中的TimeTest.cs
支持各種操作符 > ,< ,==,!=,>=,<=,-,+ 重寫

```
     var a = new Time(23, 59, 59);
     var b = new Time(23, 0, 0);
     var expected = new TimeSpan(0, 59, 59);
     TimeSpan actual;
     actual = (a - b);
     Assert.AreEqual(expected, actual);
```

超過一日的相加，會重新計算，請參考
```
  [TestMethod]
        public void AddHourMoreThan_23_hour()
        {
            int hour = 1;
            int mins = 0;
            int second = 0;
            var target = new Time(hour, mins, second);
            int hour1 = 29;
            target = target.AddHours(hour1);

            Assert.AreEqual("6:0:0", target.ToString());
        }

```