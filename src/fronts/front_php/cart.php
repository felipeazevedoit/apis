<!DOCTYPE html>
<html>
<head>
<?php include("includes/head.php") ?>

</head>

<body>

	<!-- End Preload -->
	<?php include("includes/load.php") ?>

    <!-- Header ================================================== -->
    <?php include("includes/menu.php") ?>
    <!-- End Header =============================================== -->

<!-- SubHeader =============================================== -->
<section class="parallax-window" id="short" data-parallax="scroll" data-image-src="img/sub_header_cart.jpg" data-natural-width="1400" data-natural-height="350">
    <div id="subheader">
    	<div id="sub_content">
    	 <h1>Pagamentos</h1>
            <div class="bs-wizard">
                <div class="col-xs-6 bs-wizard-step active">
                  <div class="text-center bs-wizard-stepnum"><strong>1.</strong> Detalhes do pagamento</div>
                  <div class="progress"><div class="progress-bar"></div></div>
                  <a href="#0" class="bs-wizard-dot"></a>
                </div>
                               
               <!--  <div class="col-xs-4 bs-wizard-step disabled">
                  <div class="text-center bs-wizard-stepnum"><strong>2.</strong> Forma de pagamento</div>
                  <div class="progress"><div class="progress-bar"></div></div>
                  <a href="cart_2.html" class="bs-wizard-dot"></a>
                </div> -->
            
              <div class="col-xs-6 bs-wizard-step disabled">
                  <div class="text-center bs-wizard-stepnum"><strong>2.</strong> Finalização do pedido!</div>
                  <div class="progress"><div class="progress-bar"></div></div>
                  <a href="cart_3.html" class="bs-wizard-dot"></a>
                </div>  
		</div><!-- End bs-wizard --> 
        </div><!-- End sub_content -->
	</div><!-- End subheader -->
</section><!-- End section -->

<!-- Content ================================================== -->
<div class="container margin_60_35">
		<div class="row">
			<div class="col-md-3">
				<div class="box_style_2 hidden-xs" id="help">
					<i class="icon_clock"></i>
					<h4>Horário de <span>funcionamento</span></h4>
					<!-- <a href="tel://004542344599" class="phone">+45 423 445 99</a> -->
					<h5>Segunda a sexta das 09:00 - 23:00</h5>
				</div>
                
			</div><!-- End col-md-3 -->
            
			<div class="col-md-6">
				<div class="box_style_2" id="order_process">
					<h2 class="inner">Dados pessoais</h2>
					<div class="form-group">
						<label>Nome</label>
						<input type="text" class="form-control" id="firstname_order" name="firstname_order" placeholder="Digite seu nome completo">
					</div>
					<div class="form-group">
						<label>Telenone</label>
						<input type="text" id="tel_order" name="tel_order" class="form-control" placeholder="Telefone">
					</div>
					<div class="form-group">
						<label>Whatsapp</label>
						<input type="text" id="tel_order" name="tel_order" class="form-control" placeholder="whatsapp">
					</div>
					<div class="form-group">
						<label>E-mail</label>
						<input type="email" id="email_booking_2" name="email_order" class="form-control" placeholder="E-mail">
					</div>
					<div class="form-group">
						<label>Endereço</label>
						<input type="text" id="address_order" name="address_order" class="form-control" placeholder="Endereço de entrega">
					</div>
					<div class="row">
						<div class="col-md-6 col-sm-6">
							<div class="form-group">
								<label>Cidade</label>
								<input type="text" id="city_order" name="city_order" class="form-control" placeholder="Cidade">
							</div>
						</div>
						<div class="col-md-6 col-sm-6">
							<div class="form-group">
								<label>CEP</label>
								<input type="text" id="pcode_oder" name="pcode_oder" class="form-control" placeholder="CEP">
							</div>
						</div>
					</div>
					<hr>
					
					<div class="row">
						<div class="col-md-12">
							<label>Alguma observação?</label>
							<textarea class="form-control" style="height:150px" placeholder="Digite aqui alguma observação" name="notes" id="notes"></textarea>
						</div>
					</div>
				</div>

				<div class="box_style_2">
					<h2 class="inner">Forma e pagamento</h2>
					<div class="payment_select">
						<label><input type="radio" value="" checked name="payment_method" class="icheck">Cartão de crédito</label>
						<i class="icon_creditcard"></i>
					</div>

					<div class="payment_select">
						<label><input type="radio" value="" checked name="payment_method" class="icheck">Cartão de Débito</label>
						<i class="icon_creditcard"></i>
					</div>

					<div class="payment_select">
						<label><input type="radio" value="" checked name="payment_method" class="icheck">Dinheiro</label>
						<i class="icon_wallet"></i>
					</div>

					<div class="form-group">
						<label>Troco pra quanto</label>
						<input type="text" id="city_order" name="city_order" class="form-control" placeholder="insira o valor que precisa para o troco?">
					</div>
				</div>
			</div><!-- End col-md-6 -->
            
			<div class="col-md-3" id="sidebar">
            	<div class="theiaStickySidebar">
				<div id="cart_box">
					<h3>Seus pedidos <i class="icon_cart_alt pull-right"></i></h3>
					<table class="table table_summary">
					<tbody>
					<tr>
						<td>
							<a href="#0" class="remove_item"><i class="icon_minus_alt"></i></a> <strong>1x</strong> Enchiladas
						</td>
						<td>
							<strong class="pull-right">R$11</strong>
						</td>
					</tr>
					<tr>
						<td>
							<a href="#0" class="remove_item"><i class="icon_minus_alt"></i></a> <strong>2x</strong> Burrito
						</td>
						<td>
							<strong class="pull-right">R$14</strong>
						</td>
					</tr>
					<tr>
						<td>
							<a href="#0" class="remove_item"><i class="icon_minus_alt"></i></a> <strong>1x</strong> Chicken
						</td>
						<td>
							<strong class="pull-right">R$20</strong>
						</td>
					</tr>
					<tr>
						<td>
							<a href="#0" class="remove_item"><i class="icon_minus_alt"></i></a> <strong>2x</strong> Corona Beer
						</td>
						<td>
							<strong class="pull-right">R$9</strong>
						</td>
					</tr>
					<tr>
						<td>
							<a href="#0" class="remove_item"><i class="icon_minus_alt"></i></a> <strong>2x</strong> Cheese Cake
						</td>
						<td>
							<strong class="pull-right">R$12</strong>
						</td>
					</tr>
					</tbody>
					</table>
					<hr>
					<table class="table table_summary">
					<tbody>
					<tr>
						<td>
							 Subtotal <span class="pull-right">R$56</span>
						</td>
					</tr>
					<tr>
						<td>
							valor do delivery <span class="pull-right">R$10</span>
						</td>
					</tr>
					<tr>
						<td class="total">
							 TOTAL <span class="pull-right">R$66</span>
						</td>
					</tr>
					</tbody>
					</table>
					<hr>
					<a class="btn_full" href="https://wa.me/5511999598898?text=Pedido%20Finalizado" target="_BLANK">Finalizar pedido</a>
					<a class="btn_full_outline" href="detail_page.php"><i class="icon-right"></i>Adicionar mais itens</a>
				</div><!-- End cart_box -->
                </div><!-- End theiaStickySidebar -->
			</div><!-- End col-md-3 -->
            
		</div><!-- End row -->
</div><!-- End container -->
<!-- End Content =============================================== -->

<!-- Footer ================================================== -->
<?php include("includes/footer.php") ?>
<!-- End Footer =============================================== -->

<div class="layer"></div><!-- Mobile menu overlay mask -->

<!-- Login modal -->   
<div class="modal fade" id="login_2" tabindex="-1" role="dialog" aria-labelledby="myLogin" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content modal-popup">
				<a href="#" class="close-link"><i class="icon_close_alt2"></i></a>
				<form action="#" class="popup-form" id="myLogin">
                	<div class="login_icon"><i class="icon_lock_alt"></i></div>
					<input type="text" class="form-control form-white" placeholder="Username">
					<input type="text" class="form-control form-white" placeholder="Password">
					<div class="text-left">
						<a href="#">Forgot Password?</a>
					</div>
					<button type="submit" class="btn btn-submit">Submit</button>
				</form>
			</div>
		</div>
	</div><!-- End modal -->   
    
<!-- Register modal -->   
<div class="modal fade" id="register" tabindex="-1" role="dialog" aria-labelledby="myRegister" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content modal-popup">
				<a href="#" class="close-link"><i class="icon_close_alt2"></i></a>
				<form action="#" class="popup-form" id="myRegister">
                	<div class="login_icon"><i class="icon_lock_alt"></i></div>
					<input type="text" class="form-control form-white" placeholder="Name">
					<input type="text" class="form-control form-white" placeholder="Last Name">
                    <input type="email" class="form-control form-white" placeholder="Email">
                    <input type="text" class="form-control form-white" placeholder="Password"  id="password1">
                    <input type="text" class="form-control form-white" placeholder="Confirm password"  id="password2">
                    <div id="pass-info" class="clearfix"></div>
					<div class="checkbox-holder text-left">
						<div class="checkbox">
							<input type="checkbox" value="accept_2" id="check_2" name="check_2" />
							<label for="check_2"><span>I Agree to the <strong>Terms &amp; Conditions</strong></span></label>
						</div>
					</div>
					<button type="submit" class="btn btn-submit">Register</button>
				</form>
			</div>
		</div>
	</div><!-- End Register modal -->
    
     <!-- Search Menu -->
	<div class="search-overlay-menu">
		<span class="search-overlay-close"><i class="icon_close"></i></span>
		<form role="search" id="searchform" method="get">
			<input value="" name="q" type="search" placeholder="Search..." />
			<button type="submit"><i class="icon-search-6"></i>
			</button>
		</form>
	</div>
	<!-- End Search Menu -->
    
	<?php include("includes/scripts.php") ?>

</body>
</html>