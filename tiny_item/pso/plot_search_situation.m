function plot_search_situation(glo_opt, loc_opt, particle, domain, iter)
    % ploting picture about current situation of searching
    % input : glo_opt - global optimal value
    %         loc_opt - local optimal value
    % output : none
    % ---------------------------------------------------------------------
    clf ;
    title(strcat('update in iteration', num2str(iter))) ;
    hold on ;
    axis([domain(1) domain(2) 0 6]) ;
    % ploting particle
    for i=1:size(particle, 1)
        plot(particle(i,1), particle(i,2), 'go') ;
    end
    % ploting local
    for i=1:size(loc_opt,1)
        plot(loc_opt(i,1), loc_opt(i,2), 'b*') ;
    end
    % ploting global
    plot(glo_opt(1,1), glo_opt(1,2), 'r+') ;
    hold off ;
    pause(0.5) ;
end