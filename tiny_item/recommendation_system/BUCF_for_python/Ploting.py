import matplotlib
import matplotlib.pyplot as plt

"""
Ploting class
plot different graph to demonstrate the result
"""

class Ploting() :
    """attributes"""
    rc = [] # ratio of recall
    prec = [] # ratio of precision
    cv = [] # ratio of coverage
    pop = [] # ratio of popularity
    K = [] # correspond to x

    """functions"""
    def __init__(self, perform, K) :
    #   the construction function of Ploting
    #   perform combine each varibles [[rc], [prec], [cv], [pop]]
        self.rc = [tmp[0] for tmp in perform]
        self.prec = [tmp[1] for tmp in perform]
        self.cv = [tmp[2] for tmp in perform]
        self.pop = [tmp[3] for tmp in perform]
        self.K = K
    # --- enf of __init__ ---

    def ploting(self) :
    #   ploting graphs
        # self.plot_rc_and_prec()
        # self.plot_coverage()
        self.plot_popularity()
    # --- end of ploting ---

    def plot_rc_and_prec(self) :
    #   plot recall and precision in dataSet
        fig = plt.figure()
        ax = fig.add_subplot(111)
        p1 = ax.plot(self.K, self.rc, 'ro--')
        bx = fig.add_subplot(111)
        p3 = bx.plot(self.K, self.prec, 'gs--')
        plt.title('basic-user-CF on precision&recall', fontsize=16)
        plt.xlabel('number of nearest neighbour')
        plt.ylabel('performance')
        plt.legend((p1[0], p3[0]), ('precision', 'recall'), 'best', numpoints=1)
        plt.show()
    # --- end of plot_data ---
    
    def plot_coverage(self) :
    #   plot coverage in dataSet
        fig = plt.figure()
        ax = fig.add_subplot(111)
        p1 = ax.plot(self.K, self.cv, 'bo--')
        plt.title('basic-user-CF on coverage', fontsize=16)
        plt.xlabel('number of nearest neighbour')
        plt.ylabel('performance')
        plt.legend((p1[0], p1[0]), ('coverage', 'coverage'), 'best', numpoints=1)
        plt.show()
    # --- end of plot_data ---
    
    def plot_popularity(self) :
    #   plot popularity in dataSet
        fig = plt.figure()
        ax = fig.add_subplot(111)
        p1 = ax.plot(self.K, self.pop, 'mo--')
        plt.title('basic-user-CF on popularity', fontsize=16)
        plt.xlabel('number of nearest neighbour')
        plt.ylabel('performance')
        plt.legend((p1[0], p1[0]), ('popularity', 'popularity'), 'best', numpoints=1)
        plt.show()
    # --- end of plot_data ---
    
