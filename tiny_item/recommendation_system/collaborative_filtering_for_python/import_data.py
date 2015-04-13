"""
different test set to import
"""
import string as st

def import_data(name) :
#   import rating data from name.txt
#   input : name - name of file
#   output : transction - [user item rating] as transctions

    fo = open(name)
    tmpData = [listr.strip().split('\t') for listr in fo.readlines()]
    transction = []
    for i in range(len(tmpData)) :
        tmp = []
        for j in range(len(tmpData[0])) :
            tmp.append(st.atof(tmpData[i][j]))
        transction.append(tmp)

    return transction
# --- end of import_data ---

def write_data(data, name) :
#   write data in name.txt
#   input : name - name of file
#           data - data that will write

    fo = open(name, 'w')
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
    trainData = [tmp[0:3] for tmp in trainData]
    testData = [tmp[0:3] for tmp in testData]
    for i in range(len(trainData)) :
        trainData[i][0] = int(trainData[i][0])
        trainData[i][1] = int(trainData[i][1])
    for i in range(len(testData)) :
        testData[i][0] = int(testData[i][0])
        testData[i][1] = int(testData[i][1])

    return [trainData, testData]
# --- end of train_and_test function ---
