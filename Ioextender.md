# 擴展IO功能 #

擴展出File 和 Diectory的功能


# 查詢功能 #
```
//get file using pattern
DirectoryInfo folder=new DirectoryInfo("c:\\folder");
FileInfo[] result= folder.GetFilesEx("*.txt|*.log");

//search file in the sub-Directory
DirectoryInfo folder=new DirectoryInfo("c:\\folder");
FileInfo[] result= folder.GetFilesEx("*.txt|*.log",SearchOption.AllDirectories);

```


# 自動創建目錄 #
```
DirectoryInfo folder=new DirectoryInfo("c:\\noExistFolder1\\notExistFolder2");
folder.CreateEx()

```