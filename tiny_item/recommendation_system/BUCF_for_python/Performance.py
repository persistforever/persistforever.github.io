import math

"""
Performance class
evaluation index such as :
1. recall
2. precision
3. coverage
4. popularity
"""
class Performance :
    """attributes"""
    rc = 0 # ratio of recall 
    prec = 0 # ratio of recall
    cover = 0 # ratio of coverage
    pop = 0 # ratio of popularity
    trainData = dict() # training data set
    recData = dict() # recommending data set
    testData = dict() # testing data set

    """functions"""
    def __init__(self) :
    #   the construction function of Performance
        pass
    # --- enf of __init__ ---
    
    def performance(self, trainData, recData, testData) :
    #   test performance of recommendation algorithm
    #   input : trainData - train data examples
    #           recData - recommended data examples
    #           testData - test data examples
        trainData = list2dict(trainData)
        recData = list2dict(recData)
        testData = list2dict(testData)
        self.rc = self.recall(recData, testData)
        self.prec = self.precision(recData, testData)
        self.cover = self.coverage(trainData, recData)
        self.pop = self.popularity(trainData, recData)
        return [self.rc, self.prec, self.cover, self.pop]
    # --- end of performance ---
    
    def recall(self, recData, testData) :
        # calculate recall value
        # input : recData(dict) - dict that recommendation provides {user: [item, item, ...], ...}
        #         testData(dict) - dict for test {user: [item, item, ...], ...}
        # output : recall_rate - rate of recall
        numHit = 0
        numTest = 0
        for i in recData.keys() :
            for item in recData[i] :
                if item in testData[i] :
                    numHit += 1
            numTest += len(testData[i])
        recall_rate = 1.0*numHit/numTest
        return recall_rate
    # --- end of recall ---

    def precision(self, recData, testData) :
        # calculate precision value
        # input : recData - dataSet that recommendation provides
        #         testData - dataSet for test
        # output : pre_rate - rate of precision
        numHit = 0
        numTest = 0
        for i in recData.keys() :
            for item in recData[i] :
                if item in testData[i] :
                    numHit += 1
            numTest += len(recData[i])
        pre_rate = 1.0*numHit/numTest
        return pre_rate
    # --- end of recall ---

    def coverage(self, trainData, recData) :
        # calculate how many items recommended rate
        # input : recData - dataSet that recommendation provides
        #         testData - dataSet for test
        # output : coverage - coverage of precision
        allItem = set()
        recItem = set()
        for i in recData.keys() :
            for item in recData[i] :
                recItem.add(item)
        for i in trainData.keys() :
            for item in trainData[i] :
                allItem.add(item)
        coverage = 1.0*len(recItem)/len(allItem)
        return coverage
    # --- end of coverage ---

    def popularity(self, trainData, recData) :
        # calculate items' average popularity that recommended
        # input : recData - dataSet that recommendation provides
        #         testData - dataSet for test
        # output : popularity - rate of popularity
        train_pop = dict()
        for i in trainData.keys() :
            for item in trainData[i] :
                if item not in train_pop.keys() :
                    train_pop[item] = 0
                train_pop[item] += 1
        rec_pop = dict()
        for i in recData.keys() :
            for item in recData[i] :
                if item in train_pop.keys() :
                    rec_pop[item] = train_pop[item]
                else :
                    rec_pop[item] = 0
        sum_pop = 0 
        numRec = 0
        for pop in rec_pop.values() :
            sum_pop += math.log(1+pop)
            numRec += 1
        popularity = 1.0*sum_pop/numRec
        return popularity
    # --- end of popularity ---

# --- end of Performance class ---

def list2dict(ls) :
#   create dict {user : [item, item, ...], ...} replace of list [[user, item], [user, item], ...]
    dic = dict()
    for ts in ls :
        if ts[0] not in dic.keys() :
            dic[ts[0]] = []
        dic[ts[0]].append(ts[1])
    return dic
# --- end of user_list ---
