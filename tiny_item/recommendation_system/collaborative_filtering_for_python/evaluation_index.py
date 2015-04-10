"""
evaluation index such as :
1. recall
2. precision
3. coverage
4. popularity
"""
import math

def recall(recData, testData) :
    # calculate recall value
    # input : recData(dict) - dataSet that recommendation provides
    #         testData(dict) - dataSet for test
    # output : recall_rate - rate of recall

    if len(recData.values()[0][0]) == 1 :
        numHit = 0
        numTest = 0
        for i in range(len(recData)) :
            for item in [tmp[0] for tmp in recData[i+1]] :
                if item in [tmp[0] for tmp in testData[i+1]] :
                    numHit += 1
            numTest += len(testData[i+1])
        recall_rate = 1.0*numHit/numTest
    elif len(recData.values()[0][0]) == 2 :
        numHit = 0
        numTest = 0
        for i in range(len(recData)) :
            for item in [tmp[0] for tmp in recData[i+1]] :
                if item in [tmp[0] for tmp in testData[i+1]] :
                    if recData[i+1][[tmp[0] for tmp in recData[i+1]].index(item)][1] == testData[i+1][[tmp[0] for tmp in testData[i+1]].index(item)][1] :
                        numHit += 1
            numTest += len(testData[i+1])
        recall_rate = 1.0*numHit/numTest

    return recall_rate
# --- end of recall ---

def precision(recData, testData) :
    # calculate precision value
    # input : recData - dataSet that recommendation provides
    #         testData - dataSet for test
    # output : pre_rate - rate of precision

    if len(recData.values()[0][0]) == 1 :
        numHit = 0
        numTest = 0
        for i in range(len(recData)) :
            for item in [tmp[0] for tmp in recData[i+1]] :
                if item in [tmp[0] for tmp in testData[i+1]] :
                    numHit += 1
            numTest += len(recData[i+1])
        pre_rate = 1.0*numHit/numTest
    elif len(recData.values()[0][0]) == 2 :
        numHit = 0
        numTest = 0
        for i in range(len(recData)) :
            for item in [tmp[0] for tmp in recData[i+1]] :
                if item in [tmp[0] for tmp in testData[i+1]] :
                    if recData[i+1][[tmp[0] for tmp in recData[i+1]].index(item)][1] == testData[i+1][[tmp[0] for tmp in testData[i+1]].index(item)][1] :
                        numHit += 1
            numTest += len(recData[i+1])
        pre_rate = 1.0*numHit/numTest

    return pre_rate
# --- end of recall ---

def coverage(trainData, recData) :
    # calculate how many items recommended rate
    # input : recData - dataSet that recommendation provides
    #         testData - dataSet for test
    # output : coverage - coverage of precision

    allItem = set()
    recItem = set()
    for i in range(len(recData)) :
        for item in [tmp[0] for tmp in recData[i+1]] :
            recItem.add(item)
    for i in range(len(trainData)) :
        for item in [tmp[0] for tmp in trainData[i+1]] :
            allItem.add(item)
    coverage = 1.0*len(recItem)/len(allItem)

    return coverage
# --- end of coverage ---

def popularity(trainData, recData) :
    # calculate items' average popularity that recommended
    # input : recData - dataSet that recommendation provides
    #         testData - dataSet for test
    # output : popularity - rate of popularity

    train_pop = dict()
    for i in range(len(trainData)) :
        for item in [tmp[0] for tmp in trainData[i+1]] :
            if item not in train_pop.keys() :
                train_pop[item] = 0
            train_pop[item] += 1
    rec_pop = dict()
    for i in range(len(recData)) :
        for item in [tmp[0] for tmp in recData[i+1]] :
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
