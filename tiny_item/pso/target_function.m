function y = target_function(x)
    % the target function to optimize
    % input : x - sequence of independent varibles
    % output : y - value of optimization function 
    % ---------------------------------------------------------------------
    y = 2.1*(1-x+2*x.^2).*exp(-x.^2/2) ;
end