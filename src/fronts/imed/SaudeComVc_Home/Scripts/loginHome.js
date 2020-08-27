action
$(document).ready ->
  material.init()
  
material =
  init: ->
    @bind_events()
    
  bind_events: ->
    $(document).on "click", ".button", (e) ->
      e.preventDefault()
      circle = $("<div class='circle'></div>")
      $(this).append(circle)
      x = e.pageX - $(this).offset().left - circle.width()/2
      y = e.pageY - $(this).offset().top - circle.height()/2
      size = $(this).width()
      circle.css(
        top: y+'px'
        left: x+'px'
        width: size+'px'
        height: size+'px'
      ).addClass("animate")
      setTimeout(->
        circle.remove()
      , 500)