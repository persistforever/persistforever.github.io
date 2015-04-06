function glo_opt = basic_PSO(func, domain)
    % basic PSO algorithm
    % input : func - target function
    %         domain - domain of definition(domain(1)~domain(2))
    % output : opt - optimization value
    % ---------------------------------------------------------------------
    
    % step 1 : initialize
    tf = str2func(func) ; % read target function to tf
    w = 0.6 ;
    c1 = 2.0 ;
    c2 = 2.0 ;
    vmax = 0.8 ;
    numP = 20 ; % number of particle
    precision = 1000 ;
    numIV = size(domain, 1) ; % number of independent varibles
    numIter = 20 ; % number of iterations
    glo_opt = zeros(1, numIV+1) ; % global optimal value and position
    loc_opt = zeros(numP, numIV+1) ; % local optimal value of each particle and position
    particle = zeros(numP, numIV+1) ; % matrix for particle set
    par_v = rand(numP, numIV) ; % matrix for particle velocity
    for i=1:numP
        for j=1:numIV
            particle(i,j) = random_between(domain(j,1), domain(j,2), precision) ;
        end
    end
    particle(:,end) = tf(particle(:,1:(end-1))) ;
    loc_opt = particle ;
    
    % step 2 : start ploting
    if numIV == 1
        figure ;
    end
    
    % step 3 : iterative update optimal and particle
    for i=1:numIter
        % update optimal local
        for j=1:numP
            if particle(j,end) > loc_opt(j,end)
                loc_opt(j,:) = particle(j,:) ;
            end
        end
        % update optimal global
        [~, index] = max(loc_opt(:,end)) ;
        glo_opt = loc_opt(index,:) ;
        % ploting
        if numIV == 1
            plot_search_situation(glo_opt, loc_opt, particle, domain, i) ;
        end
        
        % update particle
        for j=1:numP
            par_v(j,:) = w*par_v(j,:) + c1*(loc_opt(j,1:(end-1))-particle(j,1:(end-1))) ...
                + c2*(glo_opt(1,1:(end-1))-particle(j,1:(end-1))) ;
            for k=1:numIV
                if par_v(j,k) > vmax
                    par_v(j,k) = vmax ;
                elseif par_v(j,k) < -vmax
                    par_v(j,k) = -vmax ;
                end
            end
            particle(j,1:(end-1)) = particle(j,1:(end-1)) + par_v(j,:) ;
        end
        particle(:,end) = tf(particle(:,1:(end-1))) ;
    end
end