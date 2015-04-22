#### 各种推荐算法的测试性能图
---

* **基于用户协同过滤**：在MovieLens数据集下，读入**一组训练集**训练，读入**一组测试集**测试。使用**集合相似度**作为相似度度量。使用**最近邻**计算10个最近的用户。使用**最大推荐**（推荐感兴趣度最高的m个物品）。使用**召回率、准确率、覆盖率、流行度**进行性能测试。

<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/BUCF_for_python/result/basic-item-CF%20on%20precision&recall.png?raw=true">
<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/BUCF_for_python/result/basic-item-CF%20on%20coverage.png?raw=true">
<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/BUCF_for_python/result/basic-item-CF%20on%20popularity.png?raw=true">

* **基于物品协同过滤**：在MovieLens数据集下，读入**一组训练集**训练，读入**一组测试集**测试。使用**集合相似度**作为相似度度量。使用**最近邻**计算10个最近物品户。使用**最大推荐**（推荐感兴趣度最高的m个物品）。使用**召回率、准确率、覆盖率、流行度**进行性能测试。

<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/BICF_for_python/result/basic-item-CF%20on%20precision&recall.png?raw=true">
<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/BICF_for_python/result/basic-item-CF%20on%20coverage.png?raw=true">
<img width="33%" height="33%" src="https://github.com/persistforever/persistforever.github.io/blob/master/tiny_item/recommendation_system/BICF_for_python/result/basic-item-CF%20on%20popularity.png?raw=true">
