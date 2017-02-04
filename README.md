## LoveSave项目文档

### 为什么要做LoveSave?
QQ情侣空间的Android版app停止维护2年了,无法在Android5.0以上的环境使用.我跟老婆自相恋至今已经积累了很多爱情日志,并且希望继续记录下去.所以我决定,自己写个程序来取代QQ情侣.但是在这之前,得想办法把原来QQ情侣app中的日志弄出来.

### LoveSave的最终目的是什么?
1. 获得所有纪念日信息
2. 获得所有历史聊天记录
3. 获得所有历史日志信息(含图片)
4. **取代现有的QQ情侣空间App**

### 如何达到上述目的?
1. 解决自动登陆QQ情侣空间的问题
2. 登录后自动触发"查看更多"的问题
3. 页面加载了所有日之后抓取html代码用于解析日志
    - 如何解析html文件? `AnalysisItem类`
        - `AnalysisItem item=new AnalysisItem(str);`
        - `item.SaveToDatabase();`
        - `item.DownloadImages();`
    - 图片下载完毕后如何调用查看?
        - 将用户指定的Path保存入数据库 `Analysis类`
        - `Analysis ana=new Analysis(html,path);`
        - `ana.ImagePath="xxxx";`
4. 同理,抓取聊天记录
5. 同理,抓取纪念日信息

### 目的达到后能做什么?
1. 纪念日按时提醒
    - 什么时间?
    - 什么事?
    - 第几个周期?
    - 周期单位是?
    - 今天是宝宝5 **岁** 生日
    - 今天是结婚4 **周年** 纪念日
2. 实时聊天(留言)
    - 谁?
    - 什么时候?
    - 说了什么?
3. 查看日志
    - 数据库应该如何设计?
    - 谁? `tbItem`
    - 什么时候? `tbItem`
    - 说了什么? `tbItem`
    - 是否有图片? `tbItem`
        - 文件名? `tbImage`
        - 对应哪个item? `tbImage`
    - 是否有评论? `tbItem`
        - 谁发的? `tbComment`
        - 说的什么? `tbComment`
4. 新增日志
    
