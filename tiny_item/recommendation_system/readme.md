#### 各种推荐算法的测试性能图
---

* **基于用户协同过滤**：在MovieLens数据集下。做**欧氏距离**和**皮尔逊相似度**作为对比，评分过的物品**全部推荐**为推荐方法，最终用召回率&准确率、覆盖率和流行度进行推荐性能检测。可以发现欧氏距离在召回率和准确率上有更好的表现，而皮尔逊在覆盖率和流行度上有更好的表现，性能随着最近邻数量的增加会表现更好。

<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/collaborative_filtering_for_python/result/eurclidean%20vs%20pearson%20for%20users-CF%20on%20recall&precision.png?raw=true">
<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/collaborative_filtering_for_python/result/eurclidean%20vs%20pearson%20for%20users-CF%20on%20coverage.png?raw=true">
<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/collaborative_filtering_for_python/result/eurclidean%20vs%20pearson%20for%20users-CF%20on%20popularity.png?raw=true">

* **基于物品协同过滤**：在MovieLens数据集下，读入**一组训练集**训练，读入**一组测试集**测试。使用**集合相似度**作为相似度度量。使用**最大推荐**（推荐感兴趣度最高的m个物品）。使用**召回率、准确率、覆盖率、流行度**进行性能测试。

<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/BICF_for_python/result/basic-item-CF%20on%20coverage.png?raw=true">
<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/BICF_for_python/result/basic-item-CF%20on%20coverage.png?raw=true">
<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/collaborative_filtering_for_python/result/eurclidean%20vs%20pearson%20for%20users-CF%20on%20popularity.png?raw=true">
