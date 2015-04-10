# -*- coding: cp936 -*-
"""
import data use different train data and test data
"""

import string as st
import numpy as np
import evaluation_index as ei
import user_similarity as us
import recommendation_method as rm

def import_data(name) :
#   import rating data from name.txt
#   input : name - name of file
#   output : transction - [user item rating] as transctions

    fo = open(name)
    tmpData = [listr.strip().split('\t') for listr in fo.readlines()]
    transction = []
    for i in range(len(tmpData)) :
        tmp = []
        for j in range(3) :
            tmp.append(st.atoi(tmpData[i][j]))
        transction.append(tmp)

    return transction
# --- end of import_data ---

def write_data(data, name) :
#   write data in name.txt
#   input : name - name of file
#           data - data that will write

    fo = open(name)
    fo.writelines('\n'.join(['\t'.join(map(str, i)) for i in data]))

# --- end of write_data ---

def train_and_test(train_name, test_name) :
#   import train data and test_name
#   input : train_name - name of train data file
#           test_name - name of test data file
#   output : trainData - train data examples
#            testData - test data examples

    trainData = import_data(train_name)
    testData = import_data(test_name)

    return [trainData, testData]
# --- end of train_and_test function ---

def user_list(trainData) :
#   create user-item list
#   input : trainData - [user item rating] as transctions
#   output : UL - user list
    UL = dict()
    for ts in trainData :
        if ts[0] not in UL.keys() :
            UL[ts[0]] = []
        UL[ts[0]].append([ts[1], ts[2]])

    return UL
# --- end of user_list ---

def kNN_user(user_sim, k) :
#   find k neareast users
#   input : user_sim - user similarity
#           trainData - train data examples
#           k - number of neighbours
#   output : user_neigh - (user*k) matrix as each user for k neighbours

    user_neigh = dict()
    for i in range(len(user_sim)) :
        neighbour = dict()
        for j in range(len(user_sim)) :
            neighbour[j+1] = user_sim[i][j]
        neighbour = sorted(neighbour.iteritems(), key=lambda d:d[1], reverse = False)
        tmp = []
        for j in range(k) :
            tmp.append([neighbour[j+1][0], neighbour[j+1][1]])
        user_neigh[i+1] = tmp

    return user_neigh
# --- end of kNN_user ---

def test_performance(trainData, recData, testData) :
#   test performance of recommendation algorithm
#   input : trainData - train data examples
#           recData - recommended data examples
#           testData - test data examples
#   output : recall_rate - rate of recall
#            pre_rate - rate of precision
#            coverage - coverage of precision
#            popularity - popularity of precision

    trainData = user_list(trainData)
    recData = user_list(recData)
    testData = user_list(testData)
    recall_rate = ei.recall(recData, testData)
    pre_rate = ei.precision(recData, testData)
    coverage = ei.coverage(trainData, recData)
    popularity = ei.popularity(trainData, recData)

    print 'recall\t', recall_rate
    print 'precision\t', pre_rate
    print 'coverage\t', coverage
    print 'popularity\t', popularity
    return [recall_rate, pre_rate, coverage, popularity]
# --- end of test_performance ---

def plot_data(performance, K) :
    # plot data in dataSet
    # output : performance - correspond to y
    #          K - correspond to x

    import matplotlib
    import matplotlib.pyplot as plt
    fig = plt.figure()
    ax = fig.add_subplot(111)
    plot1 = ax.plot(K, [tmp[0] for tmp in performance], 'ro-')
    bx = fig.add_subplot(111)
    plot2 = bx.plot(K, [tmp[1] for tmp in performance], 'go-')
    # cx = fig.add_subplot(111)
    # plot3 = cx.plot(K, [tmp[2] for tmp in performance], 'bo-')
    # dx = fig.add_subplot(111)
    # plot4 = dx.plot(K, [tmp[3] for tmp in performance], 'yo-')
    plt.title('collaborative filtering user users', fontsize=16)
    plt.xlabel('number of nearest neighbour')
    plt.ylabel('performance')
    plt.legend([plot1, plot2], ('recall', 'precision'), 'best', numpoints=1)
    plt.show()
    
    return 
# --- end of plot_data ---

[trainData, testData] = train_and_test('ua.base', 'ua.test')
numUser = len(set([tmp[0] for tmp in trainData]))
numItem = len(set([tmp[1] for tmp in trainData]))
UL = user_list(trainData)
user_sim = us.euclidean_similarity(trainData, numUser)
print 'user_sim calculation finished!'
K = [5, 10, 20, 40, 80]
performance = []
for i in K :
    print 'K = ', i
    user_neigh = kNN_user(user_sim, i)
    print 'user_neigh calculation finished!'
    recData = rm.outline_recommend(UL, user_neigh, numUser, numItem, testData)
    print 'recommendation finished!'
    performance.append(test_performance(trainData, recData, testData))
plot_data(performance, K)
