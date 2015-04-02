function res = pearsonSim(vectorA, vectorB)
res = corrcoef(vectorA,vectorB) ;
res = res(1,2) ;
end