$(document).ready(function(){
    "use strict";
    var pager = $(".thumbView");
    var slideCount = 3;
    var controlerWidth = pager.parent().width();
    var slideWidth = (controlerWidth / slideCount);
    var pagerWidth = slideWidth * pager.children().length;
    pager.children().width(slideWidth);
    pager.width(pagerWidth + 1);
    if(pagerWidth < controlerWidth)
    {
        $('.pagerNext,.pagerPrev').hide();
    }
    $('.pagerNext').on("click",function()
    {   
        $('.pagerPrev').show();
        var pagerLeft = pager.position().left;
        if(Math.abs(pagerLeft) >= (pagerWidth - (slideWidth * slideCount)))
        {
            $(this).hide(); 
        }
        else
        {
            var resetLeft = (pagerLeft - slideWidth);
            pager.animate({'left':resetLeft});
        }
    });
   $('.pagerPrev').on("click",function()
    {
       $('.pagerNext').show();
        var pagerLeft = pager.position().left;
        if(pagerLeft >= 0)
        {
           $(this).hide(); 
        }
        else
        {
            var resetLeft = (pagerLeft + slideWidth);
            pager.animate({'left':resetLeft});
        }
    });
});