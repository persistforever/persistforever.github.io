import string as st

"""
ImportData class
different test set to import
"""
class ImportData :
    train_name = '' # name of training file
    test_name = '' # name of testing file

    """functions"""
    def __init__(self, train_name, test_name) :
    #   the construction function of ImportData
    #   input : train_name - name of training file
    #           test_name - name of test file
        self.train_name = train_name
        self.test_name = test_name
    # --- end of __init__ ---

    def import_data(self, name) :
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

    def write_data(self, data, name) :
    #   write data in name.txt
    #   input : name - name of file
    #           data - data that will write
        fo = open(name, 'w')
        fo.writelines('\n'.join(['\t'.join(map(str, i)) for i in data]))
    # --- end of write_data ---

    def train_and_test(self) :
    #   import train data and test_name
    #   output : trainData - train data examples
    #            testData - test data examples
        trainData = self.import_data(self.train_name)
        testData = self.import_data(self.test_name)
        trainData = [tmp[0:2] for tmp in trainData]
        testData = [tmp[0:2] for tmp in testData]
        for i in range(len(trainData)) :
            trainData[i][0] = int(trainData[i][0])
            trainData[i][1] = int(trainData[i][1])
        for i in range(len(testData)) :
            testData[i][0] = int(testData[i][0])
            testData[i][1] = int(testData[i][1])
        return [trainData, testData]
    # --- end of train_and_test function ---
