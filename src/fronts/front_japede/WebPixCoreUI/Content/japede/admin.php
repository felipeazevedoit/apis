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
				<h1>Painel administrativo</h1>
				<p>Cadastre aqui todas as informações do seu estabelecimento.</p>
				<p></p>
			</div><!-- End sub_content -->
		</div><!-- End subheader -->
	</section><!-- End section -->
	<!-- End SubHeader ============================================ -->

	<!-- Content ================================================== -->
	<div class="container margin_60">
		<div id="tabs" class="tabs">
			<nav>
				<ul>
					<li><a href="#section-1" class="icon-profile"><span>Informações principais</span></a>
					</li>
					<li><a href="#section-2" class="icon-menut-items"><span>Lista de produtos</span></a>
					</li>
					<li><a href="#section-3" class="icon-pin"><span>Delivery</span></a>
					</li>
					<li><a href="#section-4" class="icon-settings"><span>Configurações</span></a>
					</li>
				</ul>
			</nav>
			<div class="content">

				<section id="section-1">
					<div class="indent_title_in">
						<i class="icon_house_alt"></i>
						<h3>Informações gerais do estabelecimento</h3>
					</div>

					<div class="wrapper_indent">
						<div class="form-group">
							<label>Nome do estabelecmento</label>
							<input class="form-control" name="restaurant_name" id="restaurant_name" type="text" placeholder="Nome do estabelecimento">
						</div>
						<div class="form-group">
							<label>Descrição do estabelecmento</label>
							<textarea class="wysihtml5 form-control" placeholder="Descrição ..." style="height: 200px;"></textarea>
						</div>
						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
									<label>Telefone</label>
									<input type="text" id="Telephone" name="Telephone" placeholder="Telefone com DDD" class="form-control">
								</div>
							</div>
							<div class="col-sm-6">
								<div class="form-group">
									<label>Número de Whatsapp para recebimento dos pedidos</label>
									<input type="email" id="Email" name="Email" placeholder="Número do whatsapp com DDD" class="form-control">
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
									<label>E-mail</label>
									<input type="email" id="Email" name="Email" placeholder="Insira seu e-mail" class="form-control">
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
									<label>horários de funcionamento</label>
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-lg-12">
								<label>Segunda-feira</label>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-3">
								<div class="form-group">
									<label>Abertura</label>
									<input type="time" id="appt"  min="00:00" max="23:59" id="Telephone" placeholder="Abertura" name="Telephone" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Retorno do almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Retorno do almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Fechamento</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Fechamento" name="Email" class="form-control">
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-lg-12">
								<label>Terça-feira</label>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-3">
								<div class="form-group">
									<label>Abertura</label>
									<input type="time" id="appt"  min="00:00" max="23:59" id="Telephone" placeholder="Abertura" name="Telephone" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Retorno do almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Retorno do almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Fechamento</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Fechamento" name="Email" class="form-control">
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-lg-12">
								<label>Quarta-feira</label>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-3">
								<div class="form-group">
									<label>Abertura</label>
									<input type="time" id="appt"  min="00:00" max="23:59" id="Telephone" placeholder="Abertura" name="Telephone" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Retorno do almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Retorno do almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Fechamento</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Fechamento" name="Email" class="form-control">
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-lg-12">
								<label>Quinta-feira</label>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-3">
								<div class="form-group">
									<label>Abertura</label>
									<input type="time" id="appt"  min="00:00" max="23:59" id="Telephone" placeholder="Abertura" name="Telephone" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Retorno do almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Retorno do almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Fechamento</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Fechamento" name="Email" class="form-control">
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-lg-12">
								<label>Sexta-feira</label>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-3">
								<div class="form-group">
									<label>Abertura</label>
									<input type="time" id="appt"  min="00:00" max="23:59" id="Telephone" placeholder="Abertura" name="Telephone" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Retorno do almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Retorno do almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Fechamento</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Fechamento" name="Email" class="form-control">
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-lg-12">
								<label>Sábado</label>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-3">
								<div class="form-group">
									<label>Abertura</label>
									<input type="time" id="appt"  min="00:00" max="23:59" id="Telephone" placeholder="Abertura" name="Telephone" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Retorno do almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Retorno do almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Fechamento</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Fechamento" name="Email" class="form-control">
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-lg-12">
								<label>Domingo</label>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-3">
								<div class="form-group">
									<label>Abertura</label>
									<input type="time" id="appt"  min="00:00" max="23:59" id="Telephone" placeholder="Abertura" name="Telephone" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Retorno do almoço</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Retorno do almoço" name="Email" class="form-control">
								</div>
							</div>

							<div class="col-sm-3">
								<div class="form-group">
									<label>Fechamento</label>
									<input  type="time" id="appt"  min="00:00" max="23:59" id="Email" placeholder="Fechamento" name="Email" class="form-control">
								</div>
							</div>
						</div>

						<!-- <div class="row">
							<div class="col-sm-12">
								<div class="form-group">
									<div class="input_fields_wrap">
										<button class="add_field_button">Adicionar horário de</button>
										<div><input type="text" name="mytext[]" placeholder="Dia de funcionamento"  class="-function"> <input type="time" name="mytext[]" placeholder="Horário de funcionamento"   class="-function"></div>
									</div>
								</div>
							</div>
						</div> -->
					</div><!-- End wrapper_indent -->

					<hr class="styled_2">

					<div class="indent_title_in">
						<i class="icon_pin_alt"></i>
						<h3>Adicionar</h3>
					</div>
					<div class="wrapper_indent">
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
									<label>CEP</label>
									<input type="text" id="postal_code" name="postal_code" placeholder="ex. 00000-000" class="form-control">
								</div>
							</div>
						</div>
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
									<label>Endereço</label>
									<input type="text" id="street_1" name="street_1" placeholder="Digite o endereço" class="form-control">
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-sm-6">
								<div class="form-group">
									<label>Número</label>
									<input type="text" id="street_1" name="street_1" placeholder="Digie o número" class="form-control">
								</div>
							</div>

							<div class="col-sm-6">
								<div class="form-group">
									<label>Complemento</label>
									<input type="text" id="street_1" name="street_1" placeholder="Digite o complemento" class="form-control">
								</div>
							</div>
						</div>

						<div class="row">
							<div class="col-md-6">
								<div class="form-group">
									<label>Cidade</label>
									<input type="text" id="city_booking" name="city_booking" placeholder="Qual a cidade que reside" class="form-control">
								</div>
							</div>
							<div class="col-md-3">
								<div class="form-group">
									<label>Estado</label>
									<input type="text" id="state_booking" name="state_booking" placeholder="Qual o estado que reside" class="form-control">
								</div>
							</div>
							
						</div>
						<div class="row">
							<div class="col-sm-12">
								<div class="form-group">
									<label>CNPJ</label>
									<input type="text" id="Telephone" name="Telephone" placeholder="ex. 00.000.000/0000-00" class="form-control">
								</div>
							</div>
						</div><!--End row -->
					</div><!-- End wrapper_indent -->

					<hr class="styled_2">
					<div class="indent_title_in">
						<i class="icon_images"></i>
						<h3>Logotipo e capa do estabelecimento</h3>
					</div>

					<div class="wrapper_indent add_bottom_45">
						<div class="form-group">
							<label>Faça o upload do logotipo do seu estabelecimento | Tamanho máximo de 240x240 px</label>
							<div id="logo_picture" class="dropzone">
								<input name="file" type="file">
								<div class="dz-default dz-message"><span>Cique aqui ou arraste</span>
								</div>
							</div>
						</div>

						<div class="form-group">
							<label>Faça o upload da capa do seu estabelecimento | Tamanho máximo de 1400x350 px</label>
							<div id="photos" class="dropzone">
								<input name="file" type="file" multiple>
								<div class="dz-default dz-message"><span>Cique aqui ou arraste</span>
								</div>
							</div>
						</div>
					</div><!-- End wrapper_indent -->
                    
					<hr class="styled_2">
					<div class="wrapper_indent">
						<button class="btn_1">Salvar</button>
					</div><!-- End wrapper_indent -->
                    
				</section><!-- End section 1 -->

				<section id="section-2">
					<div class="indent_title_in">
						<i class="icon_document_alt"></i>
						<h3>Editar lista</h3>
					</div>
                    
					<div class="wrapper_indent">
						<div class="form-group">
							<label>Nome da categoria</label>
							<input type="text" name="menu_category" class="form-control" placeholder="EX. Entrada / Prato principal...">
						</div>

						<hr class="styled_2">

						<!-- <div class="menu-item-section clearfix">
							<h4>Menu item #1</h4>
							<div><a href="#0"><i class="icon_plus_alt"></i></a><a href="#0"><i class="icon_minus_alt"></i></a>
							</div>
						</div> -->

						<div class="strip_menu_items">
							<div class="row">
								<div class="col-sm-3">
									<label>Foto produto 1</label>
									<div class="menu-item-pic dropzone">
										<input name="file" type="file">
										<div class="dz-default dz-message"><span>Clique ou arraste<br>Foto do produto</span>
										</div>
									</div>
								</div>
								<div class="col-sm-9">
									<div class="row">
										<div class="col-md-8">
											<div class="form-group">
												<label>Nome do produto</label>
												<input type="text"  name="menu_item_title" placeholder="Informe o nome do produto" class="form-control">
											</div>
										</div>
										<div class="col-md-4">
											<div class="form-group">
												<label>Preço</label>
												<input type="text" name="menu_item_title_price" placeholder="Valor do produto" class="form-control">
											</div>
										</div>
									</div>
									<div class="form-group">
										<label>Pequena descrição</label>
										<input type="text" name="menu_item_description" placeholder="Insira uma breve descrição aqui" class="form-control">
									</div>

									<div class="form-group">
										<label>Itens adicionais</label>
										<div class="table-responsive">											
											<table id="order-item" class="table order-list table-striped edit-options">
												<tbody>
													<tr>
														<td style="width:20%">
															<input type="text" class="form-control" placeholder="+ R$3.50">
														</td>
														<td style="width:50%">
															<input type="text" class="form-control" placeholder="Ex. Hambúrguer extra">
														</td>
														<td style="width:50%"></td>
													</tr>
												</tbody>
												<tfoot>
													<tr>
														<td colspan="5" style="padding: 15px 0;">
															<input type="button" style="width: 460px;max-width: 90%;" class="btn btn-md btn-block add-row" id="addrow" value="Clique aqui para adicionar mais itens adicionais" />
														</td>
													</tr>
													<tr>
													</tr>
												</tfoot>
											</table>
										</div>
									</div><!-- End form-group -->

									<div class="form-group">
										<label>Alguma observação?</label>
										<div class="table-responsive">
											
											<table id="myTable" class="table order-list-3 table-striped edit-options">
												<tbody>
													<tr>
														<td style="width:100%">
														<textarea class="form-control" placeholder="Ex: Quero remover algum elemento do item" rows="5"></textarea>
														</td>
													</tr>
												</tbody>
											</table>
										</div>
									</div><!-- End form-group -->
								</div>
							</div><!-- End row -->
							<a href="#0" class="btn_1">Adicionar produto</a> <!-- <a href="#0" class="btn_1">Adicionar categoria</a> -->
						</div><!-- End strip_menu_items -->
					</div><!-- End wrapper_indent -->

					<hr class="styled_2">
                    
					<div class="wrapper_indent">
						<div class="add_more_cat"><a href="#0" class="btn_1">Salvar</a> </div>
					</div><!-- End wrapper_indent -->
                    
				</section><!-- End section 2 -->

				<section id="section-3">
					<div class="row">
						<div class="col-md-12 col-sm-12 add_bottom_15">
							<div class="form-group">
								<label>Valor por distância</label>
								<div class="table-responsive">											
									<table id="order-km" class="table order-list table-striped edit-options">
										<tbody>
											<tr>
												<td style="width:10%">
													<input type="text" class="form-control" placeholder="Valor">
												</td>
												<td style="width:25%">
													<input type="text" class="form-control" placeholder="distancia inicial em KM">
												</td>
												<td style="width:25%">
													<input type="text" class="form-control" placeholder="distancia final em KM">
												</td>
												<td style="width:25%"></td>
											</tr>
										</tbody>
										<tfoot>
											<tr>
												<td colspan="5" style="padding: 15px 0;">
													<input type="button" style="width: 460px;max-width: 90%;" class="btn btn-md btn-block add-row" id="addkm" value="Clique aqui para adicionar mais valores" />
												</td>
											</tr>
											<tr>
											</tr>
										</tfoot>
									</table>
								</div>
							</div><!-- End form-group -->
						</div>
						
					</div><!-- End row -->
				</section><!-- End section 3 -->

				<section id="section-4">

					<div class="row">
                    
						<div class="col-md-6 col-sm-6 add_bottom_15">
							<div class="indent_title_in">
								<i class="icon_lock_alt"></i>
								<h3>Mudar senha</h3>
							</div>
							<div class="wrapper_indent">
								<div class="form-group">
									<label>Senha antiga</label>
									<input class="form-control" name="old_password" id="old_password" type="password">
								</div>
								<div class="form-group">
									<label>Nova antiga</label>
									<input class="form-control" name="new_password" id="new_password" type="password">
								</div>
								<div class="form-group">
									<label>Confirmar nova senha</label>
									<input class="form-control" name="confirm_new_password" id="confirm_new_password" type="password">
								</div>
								<button type="submit" class="btn_1 green">Atualizar senha</button>
							</div><!-- End wrapper_indent -->
						</div>
                        
						<div class="col-md-6 col-sm-6 add_bottom_15">
							<div class="indent_title_in">
								<i class="icon_mail_alt"></i>
								<h3>Mudar e-mail</h3>
							</div>
							<div class="wrapper_indent">
								<div class="form-group">
									<label>E-mail antigo</label>
									<input class="form-control" name="old_email" id="old_email" type="email">
								</div>
								<div class="form-group">
									<label>Novo e-mail</label>
									<input class="form-control" name="new_email" id="new_email" type="email">
								</div>
								<div class="form-group">
									<label>Confirmar novo e-mail</label>
									<input class="form-control" name="confirm_new_email" id="confirm_new_email" type="email">
								</div>
								<button type="submit" class="btn_1 green">Atualizar e-mail</button>
							</div><!-- End wrapper_indent -->
						</div>
					</div><!-- End row -->
				</section><!-- End section 3 -->

			</div><!-- End content -->
		</div>
	</div><!-- End container  -->
	<!-- End Content =============================================== -->

	<!-- Footer ================================================== -->
	<?php include("includes/footer.php")?>
	<!-- End Footer =============================================== -->

	<div class="layer"></div>
	<!-- Mobile menu overlay mask -->

	<!-- Login modal -->
	<div class="modal fade" id="login_2" tabindex="-1" role="dialog" aria-labelledby="myLogin" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content modal-popup">
				<a href="#" class="close-link"><i class="icon_close_alt2"></i></a>
				<form action="#" class="popup-form" id="myLogin">
					<div class="login_icon"><i class="icon_lock_alt"></i>
					</div>
					<input type="text" class="form-control form-white" placeholder="Username">
					<input type="text" class="form-control form-white" placeholder="Password">
					<div class="text-left">
						<a href="#">Forgot Password?</a>
					</div>
					<button type="submit" class="btn btn-submit">Submit</button>
				</form>
			</div>
		</div>
	</div>
	<!-- End modal -->

	<!-- Register modal -->
	<div class="modal fade" id="register" tabindex="-1" role="dialog" aria-labelledby="myRegister" aria-hidden="true">
		<div class="modal-dialog">
			<div class="modal-content modal-popup">
				<a href="#" class="close-link"><i class="icon_close_alt2"></i></a>
				<form action="#" class="popup-form" id="myRegister">
					<div class="login_icon"><i class="icon_lock_alt"></i>
					</div>
					<input type="text" class="form-control form-white" placeholder="Name">
					<input type="text" class="form-control form-white" placeholder="Last Name">
					<input type="email" class="form-control form-white" placeholder="Email">
					<input type="text" class="form-control form-white" placeholder="Password" id="password1">
					<input type="text" class="form-control form-white" placeholder="Confirm password" id="password2">
					<div id="pass-info" class="clearfix"></div>
					<div class="checkbox-holder text-left">
						<div class="checkbox">
							<input type="checkbox" value="accept_2" id="check_2" name="check_2" />
							<label for="check_2"><span>I Agree to the <strong>Terms &amp; Conditions</strong></span>
							</label>
						</div>
					</div>
					<button type="submit" class="btn btn-submit">Register</button>
				</form>
			</div>
		</div>
	</div>
	<!-- End Register modal -->

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