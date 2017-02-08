# LoveSave文档
---
## 2017年2月9日 最新成果

### Http请求
- 请求类型Get
- 请求全文`http://sweet.snsapp.qq.com/v2/cgi-bin/sweet_share_getbyhouse?g_tk=615775281&uin=914724771&start=0&num=10&opuin=914724771&plat=0&outputformat=4`
- 重要参数:要获取的日志,从0开始编号,本次请求要获取的为`start=?`开始的连续`num=?`个
- 注意事项:0~200号可以一次性请求,200号之后的一次不能超过50个

### 获取到的json文件解析

##### Json Root
- total 总共有多少条日志
- data[] 数组存储日志信息
    1. content 日志内容
    2. nick 发布人昵称
    3. time 发布时间(时间戳)
    4. uin 发布人QQ号
    5. **com_total**是否包含评论:等于0时不包含,不等于0时包含
    6. comments[] 数组存储评论信息
        - id : 标识评论在日志中的次序
        - content 
        - nick
        - time
        - uin
        - data[] 未猜解成功:目前发现所有相册上传图片时所自动生成的日志,其下所有评论包含一个空的data[],感觉像是开发人员的bug,因为每个日志都包含...
        - reply_total 评论是否包含评论:为0时不包含,否则包含
        - reply[] ""评论的评论""信息数组
            - content
            - nick
            - time
            - uin
    7. richval 日志包含图片相关信息,JS数组对象,这里按字符串处理
    
### 解析逻辑伪码
```
JsonObject jo = AanalysisJsonFile(jsonFile);
foreach(var jodata in jo["data"])
{
    GetContent();
    GetNick();
    GetTime(ConvertTime());
    GetUin();
    if(GetRichval()!="")
    {
        List<ImageUrl> list = AanalysisRichval();
    }
    if(GetComTotal()!=0)
    {
        foreach(var jodcom in jodata["comments"])
        {
            GetId();
            GetContent();
            GetNick();
            GetTime();
            GetUin();
            if(GetReplyTotal()!=0)
            {
                foreach(var jodcreply in jodcom["reply"])
                {
                    GetContent();
                    GetNick();
                    GetTime();
                    GetUin();
                }
            }
        }
    }
}

```
## 项目主思路
### 为什么要做LoveSave?
QQ情侣空间的Android版app停止维护2年了,无法在Android5.0以上的环境使用.我跟老婆自相恋至今已经积累了很多爱情日志,并且希望继续记录下去.所以我决定,自己写个程序来取代QQ情侣.但是在这之前,得想办法把原来QQ情侣app中的日志弄出来.

### LoveSave的最终目的是什么?
1. 获得所有纪念日信息(Done)
2. 获得所有历史聊天记录(Done)
3. 获得所有历史日志信息(Done)
4. **取代现有的QQ情侣空间App**

### 如何达到上述目的?
1. 解决自动登陆QQ情侣空间的问题
2. 登录后自动触发"查看更多"的问题(Done 找到get请求,及相关参数)
3. 页面加载了所有日志之后抓取html代码用于解析日志
    - 找到了更好的解析资源:上述get请求反回的json文件.
        ~~如何解析html文件?**(Done)**`AnalysisItem类``AnalysisItem item=new AnalysisItem(str);`~~
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
    - 数据库应该如何设计?**(Done)**
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

    
