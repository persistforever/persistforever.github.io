#### 各种推荐算法的测试性能图
---

* 基于用户协同过滤：在MovieLens数据集下。做**欧氏距离**和**皮尔逊相似度**作为对比，评分过的物品**全部推荐**为推荐方法，最终用召回率&准确率、覆盖率和流行度进行推荐性能检测。可以发现欧氏距离在召回率和准确率上有更好的表现，而皮尔逊在覆盖率和流行度上有更好的表现，性能随着最近邻数量的增加会表现更好。

<img width="33%" height="33%" src="http://d.pcs.baidu.com/thumbnail/b4d72ec81e3e5d4663003ddb24163598?fid=605430473-250528-797767335626412&time=1428890400&sign=FDTAER-DCb740ccc5511e5e8fedcff06b081203-21OLZumXcO3NnNbzXkruPQWMumA%3D&rt=sh&expires=2h&r=917560202&sharesign=unknown&size=c710_u500&quality=100">
<img width="33%" height="33%" src="http://d.pcs.baidu.com/thumbnail/f918b946f4e512b759b7f49369cc160f?fid=605430473-250528-175874910539335&time=1428890400&sign=FDTAER-DCb740ccc5511e5e8fedcff06b081203-eJAdz0%2Bo%2FINP5CXDO7IkPX4p5cU%3D&rt=sh&expires=2h&r=404108445&sharesign=unknown&size=c710_u500&quality=100">
<img width="33%" height="33%" src="http://d.pcs.baidu.com/thumbnail/361e1ed6264fadd3d0d72dcc93218388?fid=605430473-250528-448507407667522&time=1428890400&sign=FDTAER-DCb740ccc5511e5e8fedcff06b081203-f5tcLsdRntK7j%2B8QbQ1CZyT9RsQ%3D&rt=sh&expires=2h&r=955006704&sharesign=unknown&size=c710_u500&quality=100">

